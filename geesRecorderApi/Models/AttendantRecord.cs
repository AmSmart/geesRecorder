using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderApi.Models
{
    public record AttendantRecord
    {
        public bool Attended { get; set; }

        public DateTime? TimeStamp { get; set; }

        public virtual Attendant Attendant { get; set; }

        public virtual AttendanceEvent AttendanceEvent { get; set; }
    }
}
