using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace geesRecorder.Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Pin { get; set; }

        // Navigation Property - Many(One to Many)
        public virtual ICollection<Attendance> Attendances { get; set; }

        // Navigation Property - Many to Many
        public virtual ICollection<DataCollection> DataCollections { get; set; }

        public List<string> ProjectPermissions { get; set; }
    }
}
