using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Appointment : Entity<int>
    {
        public int UserId { get; set; }

        public int AppointmentIntervalId { get; set; }
        public int? FeedbackId { get; set; }
        public int? ReportId { get; set; }

        // Navigation properties
        public virtual User User { get; set; }

        public virtual AppointmentInterval AppointmentInterval { get; set; }

        public virtual Feedback Feedback { get; set; }
        public virtual Report Report { get; set; }

    }
}
