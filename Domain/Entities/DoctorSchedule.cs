using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DoctorSchedule : Entity<int>
    {
        public int DoctorId { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan PatientInterval { get; set; } // in minutes

        // Navigation properties
        public virtual Doctor Doctor { get; set; }

     
    }
}
