using Core.Domain;

namespace Domain.Entities
{
    public class Title : Entity<short>
    {
        public string Name { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
