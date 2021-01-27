using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderApi.Models
{
    public record Attendance
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public virtual List<Attendant> Attendants { get; set; }

        public virtual List<ApplicationUser> ApplicationUser { get; set; }

        public virtual List<AttendanceEvent> Events { get; set; }
    }
}
