namespace Domain.Entities
{
    public class OperationClaim : Core.Security.Entitites.OperationClaim
    {
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
