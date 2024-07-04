using Core.Domain;
using Core.Utilities.EncryptionHelper;

namespace Core.Security.Entitites
{
    public class BaseUser : Entity<int>
    {
        private string? _firstName;
        private string? _lastName;
        private string? _email;
        private string? _phoneNumber;


        public string? FirstName
        {
            get => _firstName == null ? null : EncryptionHelper.Decrypt(_firstName);
            set => _firstName = value == null ? null : EncryptionHelper.Encrypt(value);
        }

        public string? LastName
        {
            get => _lastName == null ? null : EncryptionHelper.Decrypt(_lastName);
            set => _lastName = value == null ? null : EncryptionHelper.Encrypt(value);
        }

        public string? Email
        {
            get => _email == null ? null : EncryptionHelper.Decrypt(_email);
            set => _email = value == null ? null : EncryptionHelper.Encrypt(value);
        }

        public string? PhoneNumber
        {
            get => _phoneNumber == null ? null : EncryptionHelper.Decrypt(_phoneNumber);
            set => _phoneNumber = value == null ? null : EncryptionHelper.Encrypt(value);
        }

        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; } = true;

    }
}
