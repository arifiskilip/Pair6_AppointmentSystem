using Core.Security.Entitites;

namespace Domain.Entities
{
    public class User : BaseUser
    {
        public DateTime BirthDate { get; set; }
        public bool IsEmailVerified { get; set; } = false;
        public string? IdentityNumber { get; set; }
        public string? ImageUrl { get; set; }

        public short GenderId { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }

        public virtual Gender Gender { get; set; }

    }
}
