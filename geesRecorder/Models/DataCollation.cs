using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Models
{
    public class DataCollation
    {
        public string Id { get; set; }

        public string TextFieldsSchema { get; set; }

        public string NumberFieldsSchema { get; set; }

        public string SelectionFieldsSchema { get; set; }

        [Column(TypeName = "jsonb")]
        public virtual ICollection<Record> Records { get; set; }
    }
}
