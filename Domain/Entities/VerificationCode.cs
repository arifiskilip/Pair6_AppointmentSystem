using Core.Domain;

namespace Domain.Entities
{
    public class VerificationCode : Entity<int>
    {
        public string Code { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CodeTypeId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual CodeType CodeType { get; set; }
    }
}
