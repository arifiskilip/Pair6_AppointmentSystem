using Core.Domain;

namespace Domain.Entities
{
    public class OperationClaim : Entity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
