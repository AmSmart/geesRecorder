using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Api.Models
{
    public class Record
    {
        public ICollection<TextField> TextFields { get; set; }

        public ICollection<NumberField> NumberFields { get; set; }

        public ICollection<SelectionField> SelectionFields { get; set; }
    }
}
