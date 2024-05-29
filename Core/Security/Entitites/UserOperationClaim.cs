using Core.Domain;

namespace Core.Security.Entitites
{
    public class UserOperationClaim : Entity<int>
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

    }
}
