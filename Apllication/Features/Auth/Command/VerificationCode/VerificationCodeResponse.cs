namespace Application.Features.Auth.Command.VerificationCode
{
    public class VerificationCodeResponse
    {
        public string Code { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CodeTypeId { get; set; }
        public int UserId { get; set; }
    }

}
