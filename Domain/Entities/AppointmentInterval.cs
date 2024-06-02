using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AppointmentInterval : Entity<int>
    {
        public int DoctorId { get; set; }

        public DateTime Day { get; set; }

        public TimeSpan IntervalStart { get; set; }
        public TimeSpan IntervalEnd { get; set; }

        public short AppointmentStatusId { get; set; }

        public virtual AppointmentStatus AppointmentStatus { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual Appointment Appointment { get; set; }


    }
}
