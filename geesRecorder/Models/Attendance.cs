using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Models
{
    public class Attendance
    {
        public string Id { get; set; }

        public string Name { get; set; }

        // Navigation Property - Many(One to Many)
        public virtual ICollection<Attendant> Attendants { get; set; }

        // Navigation Property - One(One to Many)
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Column(TypeName = "jsonb")]
        public ICollection<AttendanceEvent> EventTracker { get; set; }
    }
}
