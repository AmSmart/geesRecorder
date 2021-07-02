using geesRecorder.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Shared.DTOs
{
    public record EnrolPersonDTO
    {
        [Required]        
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string CustomId { get; set; }        

        public int ProjectId { get; set; }

        public int FingerprintId { get; set; }

        public bool PersonAlreadyExists { get; set; }
    }
}
