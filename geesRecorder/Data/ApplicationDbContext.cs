using geesRecorder.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace geesRecorder.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<DataCollection> DataCollections { get; set; }

        public DbSet<AttendanceEvent> AttendanceEvents { get; set; }

        public DbSet<AttendantRecord> AttendantRecords { get; set; }

        public DbSet<Attendant> Attendants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
