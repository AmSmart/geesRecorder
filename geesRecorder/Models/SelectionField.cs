using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace geesRecorder.Models
{
    public class SelectionField
    {
        //public Type FieldType { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "jsonb")]
        public ICollection<string> ValuesList { get; set; }

        public string SelectedValue { get; set; }
    }
}