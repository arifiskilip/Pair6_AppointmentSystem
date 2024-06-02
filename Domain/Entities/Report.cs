using Core.Domain;

namespace Domain.Entities
{
    public class Report :Entity<int>
    {
        public int AppointmentId { get; set; }
        public string Description { get; set; }
        public string ReportFile { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}
