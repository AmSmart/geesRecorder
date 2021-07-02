using geesRecorder.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Shared.DTOs
{
    public record CreateProjectDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }
    }
}
