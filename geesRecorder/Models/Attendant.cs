using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace geesRecorder.Models
{
    public class Attendant
    {
        public string Id { get; set; }

        public string Name { get; set; }

        // Navigation Property - One(One to Many)
        public virtual Attendance Attendance { get; set; }

        // Navigation Property - Many(One to Many)
        public virtual ICollection<AttendanceEvent> Events { get; set; }        
    }
}