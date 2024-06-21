namespace Application.Features.Auth.Command.IsEmailVerified
{
    public class IsEmailVerifiedResponse
    {
        public int UserId { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
