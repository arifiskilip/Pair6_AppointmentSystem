using Core.Domain;

namespace Domain.Entities
{
    public class AppointmentInterval : Entity<int>
    {
        public int DoctorId { get; set; }
        public DateTime IntervalDate { get; set; }
        public short AppointmentStatusId { get; set; }

        public virtual AppointmentStatus AppointmentStatus { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
