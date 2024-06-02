using Core.Domain;

namespace Domain.Entities
{
    public class Feedback :Entity<int>
    {
        public int PatientId { get; set; }
        public string Description { get; set; }
        public int AppointmentId { get; set; }
        public bool Status { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}
