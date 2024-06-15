using Core.Domain;

namespace Domain.Entities
{
    public class Appointment : Entity<int>
    {
        public int PatientId { get; set; }
        public int AppointmentIntervalId { get; set; }
        public int? FeedbackId { get; set; }
        public int? ReportId { get; set; }
        public short AppointmentStatusId { get; set; }

        // Navigation properties
        public virtual Patient Patient { get; set; }
        public virtual AppointmentInterval AppointmentInterval { get; set; }
        public virtual Feedback? Feedback { get; set; }
        public virtual Report? Report { get; set; }

        public virtual AppointmentStatus AppointmentStatus { get; set; }

    }
}
