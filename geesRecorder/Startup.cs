using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using geesRecorder.Data;
using geesRecorder.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using geesRecorder.Services;
using Microsoft.AspNetCore.HttpOverrides;
using AutoMapper;
using geesRecorder.Interfaces;

namespace geesRecorder
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Environment.GetEnvironmentVariable("GEES_REC_DATABASE_URL");
            connectionString += ";sslmode=Require;Trust Server Certificate=true;";
            string thisAssemblyName = typeof(Startup).Assembly.FullName;

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Parse connection URL to connection string for Npgsql
                connectionString = connectionString.Replace("postgres://", string.Empty);

                var pgUserPass = connectionString.Split("@")[0];
                var pgHostPortDb = connectionString.Split("@")[1];
                var pgHostPort = pgHostPortDb.Split("/")[0];

                var pgDb = pgHostPortDb.Split("/")[1];
                var pgUser = pgUserPass.Split(":")[0];
                var pgPass = pgUserPass.Split(":")[1];
                var pgHost = pgHostPort.Split(":")[0];
                var pgPort = pgHostPort.Split(":")[1];

                var connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb}";

                options.UseLazyLoadingProxies();
                options.UseNpgsql(connStr);
            });            

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services.AddIdentityServer(config =>
            {
                config.UserInteraction.ErrorUrl = "/Error";
            })
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(config => 
                {
                    config.Clients[0].UpdateAccessTokenClaimsOnRefresh = true;
                    config.Clients[0].UpdateAccessTokenClaimsOnRefresh = true;
                    config.Clients[0].AlwaysIncludeUserClaimsInIdToken = true;
                    config.Clients[0].AccessTokenLifetime = 3600;
                    config.Clients[0].AlwaysSendClientClaims = true;
                })
                .AddDeveloperSigningCredential()
                .AddProfileService<AppProfileService>();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedHost |     //Not included in the defaults using ASPNETCORE_FORWARDEDHEADERS_ENABLED
                    ForwardedHeaders.XForwardedFor |
                    ForwardedHeaders.XForwardedProto;
                options.ForwardLimit = 2;
                options.KnownNetworks.Clear(); //In a real scenario we would add the real proxy network(s) here based on a config parameter
                options.KnownProxies.Clear();  //In a real scenario add the real proxy here based on a config parameter
            });

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddScoped<IAttendanceManager, AttendanceManager>();
            services.AddScoped<IPermissionManager, PermissionManager>();
            services.AddScoped<IDataCollectionManager, DataCollectionManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change thizs for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                    spa.Options.StartupTimeout = TimeSpan.FromSeconds(300);
                }
            });
        }
    }
}
