namespace Domain.Entities
{
    public class UserOperationClaim : Core.Security.Entitites.UserOperationClaim
    {
        public virtual User User { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
    }
}
