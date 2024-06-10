using Core.Security.Entitites;

namespace Domain.Entities
{
    public class User : BaseUser
    {
        public DateTime BirthDate { get; set; }
        public string? IdentityNumber { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }

    }
}
