using Core.Domain;

namespace Domain.Entities
{
    public class AppointmentStatus : Entity<short>
    {
        public string Name { get; set; }

        public virtual ICollection<AppointmentInterval> AppointmentIntervals { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
