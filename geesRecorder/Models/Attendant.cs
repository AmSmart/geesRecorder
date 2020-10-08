using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace geesRecorder.Models
{
    public class Attendant
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "jsonb")]
        public virtual ICollection<AttendanceEvent> Events { get; set; }        
    }
}