using geesRecorder.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace geesRecorder.Client.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<PersonEvent> PersonEvents { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<AttendanceProject> AttendanceProjects { get; set; }

        public DbSet<DataCollectionProject> DataCollectionProjects { get; set; }

        public DbSet<ServerState> ServerState { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Response> Responses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .Property(x => x.FingerprintIds)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<List<int>>(v, new JsonSerializerOptions()),
                    new ValueComparer<List<int>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));

            modelBuilder.Entity<Question>()
                .Property(x => x.Options)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<List<string>>(v, new JsonSerializerOptions()),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));
            
            modelBuilder.Entity<Response>()
                .Property(x => x.Answers)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<List<string>>(v, new JsonSerializerOptions()),
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));

            modelBuilder.Entity<Project>()
                .HasDiscriminator(x => x.Type)
                .HasValue<Project>(ProjectType.None)
                .HasValue<AttendanceProject>(ProjectType.Attendance)
                .HasValue<DataCollectionProject>(ProjectType.DataCollection);            
        }

        public async Task<DBSync> GetDBSnapshot()
        {
            var attendanceProjects = await AttendanceProjects.ToListAsync();
            var dataCollections = await DataCollectionProjects.ToListAsync();
            var personEvents = await PersonEvents.ToListAsync();
            var attendaceRegisters = new List<AttendanceRegister>();

            foreach (var project in attendanceProjects)
            {
                var attendanceRegister = new AttendanceRegister
                {
                    Name = project.Name,
                    Events = project.Events.Select(x => new AttendanceEventRecord
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Start = x.Start,
                        End = x.End,
                        Attendants = project.Persons.Select(y => 
                        {
                            var personEvent = personEvents.FirstOrDefault(z => z.Event.Id == x.Id && z.Person.Id == y.Id);
                            var person = new AttendancePerson
                            {
                                Id = y.Id,
                                CustomId = y.CustomId,
                                FirstName = y.FirstName,
                                LastName = y.LastName,
                                TimeIn = personEvent.TimeIn,
                                TimeOut = personEvent.TimeOut
                            };
                            return person;
                        }).ToList()
                    }).ToList(),
                };

                attendaceRegisters.Add(attendanceRegister);
            }

            var dbSync = new DBSync
            {
                DataCollectionRecords = dataCollections,
                AttendanceRegisters = attendaceRegisters
            };

            return dbSync;
        }
    }
}
