using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderApi.Models
{
    public record AttendanceEvent
    {
        public string Id { get; set; }

        public string Name { get; set; }        

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public virtual Attendance Attendance { get; set; }

        public virtual List<AttendantRecord> AttendantRecords { get; set; }
    }
}