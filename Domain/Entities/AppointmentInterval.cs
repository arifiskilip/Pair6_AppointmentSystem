using Core.Domain;

namespace Domain.Entities
{
    public class AppointmentInterval : Entity<int>
    {
        public int DoctorId { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan Interval { get; set; }
        public short AppointmentStatusId { get; set; }

        public virtual AppointmentStatus AppointmentStatus { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Appointment? Appointment { get; set; }
    }
}
