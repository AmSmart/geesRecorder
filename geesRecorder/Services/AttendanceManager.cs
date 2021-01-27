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

        public async Task CreateAttendanceEvent(int attendanceId, string name, DateTime start, DateTime end)
        {
            var attendance = await _dbContext.Attendances.FindAsync(attendanceId);
            attendance.Events.Add(new AttendanceEvent
            {
                Name = name,
                Start = start,
                End = end,
                AttendantRecords = attendance.Attendants.Select(x => new AttendantRecord
                {
                    Attendant = x,                    
                })
                .ToList()
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateAttendanceAsync(string actorName, string attendanceName)
        {
            var attendance = new Attendance
            {
                Name = attendanceName
            };

            (await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == actorName))
                .Attendances
                .Add(attendance);

            await _dbContext.SaveChangesAsync();

        }

        public async Task EnrollAttendantsAsync(int attendanceId, List<Attendant> attendants)
        {
            var attendance = await _dbContext.Attendances.FindAsync(attendanceId);

            foreach (var attendant in attendants)
            {
                foreach (var @event in attendance.Events)
                {
                    attendant.AttendantRecords.Add(new AttendantRecord
                    {
                        AttendanceEvent = @event
                    });
                }
            }

            attendance.Attendants.AddRange(attendants);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Attendance> GetAttendanceAsync(int attendanceId)
            => await _dbContext.Attendances.FindAsync(attendanceId);

        public async Task MarkAttendanceAsync(string eventId, int attendantId)
        {
            var attendanceEvent = await _dbContext.AttendanceEvents.FindAsync(eventId);
            var attendanceRecord = attendanceEvent.AttendantRecords.First(x => x.Attendant.Id == attendantId);

            attendanceRecord.Attended = true;
            attendanceRecord.TimeStamp = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAttendanceEventAsync(int eventId, DateTime start, DateTime end)
        {
            var attendanceEvent = await _dbContext.AttendanceEvents.FindAsync(eventId);
            
            attendanceEvent.Start = start;
            attendanceEvent.End = end;
                        
            await _dbContext.SaveChangesAsync();
        }
    }
}