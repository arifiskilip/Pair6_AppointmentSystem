using Core.Domain;

namespace Domain.Entities
{
    public class CodeType : Entity<int>
    {
        public string Name { get; set; }


        public virtual ICollection<VerificationCode> VerificationCodes { get; set; }
    }
}
