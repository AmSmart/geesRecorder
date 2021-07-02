using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using geesRecorder.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace geesRecorder.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
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
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
