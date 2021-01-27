using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace geesRecorder.Models
{
    public class Attendant
    {
        public int Id { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public virtual Attendance Attendance { get; set; }

        public virtual List<AttendantRecord> AttendantRecords { get; set; }
    }
}