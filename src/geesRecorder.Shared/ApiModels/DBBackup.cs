using geesRecorder.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Shared.ApiModels
{
    public class DBBackup
    {
        public int Id { get; set; }

        public string MachineName { get; set; }

        public string MachineCode { get; set; }

        [Column(TypeName = "jsonb")]
        public DBSnapshot DBSnapshot { get; set; }
    }
}