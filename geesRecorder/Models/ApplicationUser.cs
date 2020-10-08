using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace geesRecorder.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }

        public virtual ICollection<DataCollation> DataCollations { get; set; }
    }
}
