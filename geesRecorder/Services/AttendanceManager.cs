using geesRecorder.Data;
using geesRecorder.Interfaces;
using geesRecorder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Services
{
    public class AttendanceManager : IAttendanceManager
    {
        private readonly ApplicationDbContext _dbContext;

        public AttendanceManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CloseAttendanceEventAsync(string eventName)
        {
            var attendanceEvents = _dbContext.AttendanceEvents.Where(x => x.Name == eventName);

            foreach (var @event in attendanceEvents)
            {
                @event.Deadline = DateTime.Now;
            }
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateAttendanceAsync(string actorName, string attendanceName, ICollection<Attendant> attendants)
        {
            var attendance = new Attendance
            {
                Name = attendanceName,
                Attendants = attendants
            };

            (await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == actorName))
                .Attendances
                .Add(attendance);

            await _dbContext.SaveChangesAsync();

        }

        public async Task<Attendance> GetAttendanceAsync(string actorName, string attendanceName)        
            => (await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == actorName))
            .Attendances
            .FirstOrDefault(x => x.Name == attendanceName);        

        public async Task MarkAttendanceAsync(string eventId, bool attended)
        {
            var attendeanceEvent = (await _dbContext.AttendanceEvents.FindAsync(eventId));
            attendeanceEvent.Attended = attended;
            _dbContext.SaveChanges();                
        }

        public async Task SetAttendanceEventTimeoutAsync(string eventName, int timeInMinutes)
        {
            var attendanceEvents = _dbContext.AttendanceEvents.Where(x => x.Name == eventName);

            foreach (var @event in attendanceEvents)
            {
                @event.Deadline = DateTime.Now.AddMinutes(timeInMinutes);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
