using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Shared.DTOs
{
    public record PersonDetailsDTO
    {
        public string Name { get; set; }

        public int TotalEventsAttended { get; set; }

        public int TotalEventsMissed { get; set; }
    }
}
