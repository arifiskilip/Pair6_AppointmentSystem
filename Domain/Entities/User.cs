using Core.Security.Entitites;
using Core.Utilities.EncryptionHelper;

namespace Domain.Entities
{
    public class User : BaseUser
    {
        private string? _identityNumber;


        public DateTime BirthDate { get; set; }
        public bool IsEmailVerified { get; set; } = false;
        public string? IdentityNumber
        {
            get => _identityNumber == null ? null : EncryptionHelper.Decrypt(_identityNumber);
            set => _identityNumber = value == null ? null : EncryptionHelper.Encrypt(value);
        }
        public string? ImageUrl { get; set; }

        public short GenderId { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }

        public virtual Gender Gender { get; set; }

    }
}
