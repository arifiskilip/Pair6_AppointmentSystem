using Core.Domain;
using Domain.Enums;

namespace Domain.Entities
{
    public class Branch : Entity<short>
    {
        public string Name { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
