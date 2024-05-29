using Core.Domain;

namespace Domain.Entities
{
    public class AppointmentStatus : Entity<short>
    {
        public string Name { get; set; }
    }
}
