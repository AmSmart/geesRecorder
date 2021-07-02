﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Api.Models
{
    public record AttendantRecord
    {
        public int Id { get; set; }

        public bool Attended { get; set; }

        public DateTime? TimeStamp { get; set; }

        public virtual Attendant Attendant { get; set; }

        public virtual AttendanceEvent AttendanceEvent { get; set; }
    }
}