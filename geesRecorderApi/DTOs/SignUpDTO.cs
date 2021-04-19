using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderApi.DTOs
{
    public record SignUpDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(4, MinimumLength = 4)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Pin must be numeric")]
        public string Pin { get; set; }
    }
}
