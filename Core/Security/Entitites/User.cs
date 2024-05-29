using Core.Domain;

namespace Core.Security.Entitites
{
    public class User : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentiyNumber { get; set; } //Tc No
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; } = true;


        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
