using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Shared.Models
{
    public class DBSync
    {
        public List<DataCollectionProject> DataCollectionRecords { get; set; }

        public List<AttendanceRegister> AttendanceRegisters { get; set; }
    }

    public class AttendanceRegister
    {
        public string Name { get; set; }

        public List<AttendanceEventRecord> Events { get; set; }
    }

    public class AttendanceEventRecord
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public List<AttendancePerson> Attendants { get; set; }
    }

    public class AttendancePerson
    {
        public int Id { get; set; }

        public string CustomId { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? TimeIn { get; set; }

        public DateTime? TimeOut { get; set; }
    }
}
