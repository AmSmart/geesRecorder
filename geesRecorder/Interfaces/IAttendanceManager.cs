using geesRecorder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Interfaces
{
    public interface IAttendanceManager
    {
        Task CloseAttendanceEventAsync(string eventName);

        Task CreateAttendanceAsync(string actorName, string attendanceName, ICollection<Attendant> attendants);

        Task<Attendance> GetAttendanceAsync(string actorName, string attendanceId);

        Task MarkAttendanceAsync(string eventId, bool attended);

        Task SetAttendanceEventTimeoutAsync(string eventName, int timeInMinutes);
    }
}
