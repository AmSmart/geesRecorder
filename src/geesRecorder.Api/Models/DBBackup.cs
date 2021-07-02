using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Api.Models
{
    public class DBBackup
    {
        public int Id { get; set; }

        public string MachineName { get; set; }

        public string MachineCode { get; set; }
    }
}
