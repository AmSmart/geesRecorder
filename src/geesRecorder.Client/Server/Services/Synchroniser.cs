﻿using geesRecorder.Shared.ApiModels;
using geesRecorder.Client.Server.Data;
using geesRecorder.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace geesRecorder.Client.Server.Services
{
    public class Synchroniser : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly HttpClient _httpClient;
        private Timer _timer;

        public Synchroniser(IHttpClientFactory httpClientFactory,
            IServiceScopeFactory serviceScopeFactory,
            IWebHostEnvironment webHostEnvironment)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _webHostEnvironment = webHostEnvironment;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(ApiRoutes.ApiBaseAddress);
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async o =>
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                string dbPath = Path.Combine(_webHostEnvironment.ContentRootPath, "geesRecorder.db");
                if (File.Exists(dbPath))
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var dbSnapshot = await dbContext.GetDBSnapshot();

                    var dbBackup = new DBBackup
                    {
                        DBSnapshot = dbSnapshot,
                        MachineCode = Environment.MachineName,
                        MachineName = Environment.UserName,
                    };
                    await _httpClient.PostAsJsonAsync("auth/sync", dbBackup);
                }                
            }
        },
        null,
        TimeSpan.Zero,
        TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
