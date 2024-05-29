namespace Core.Mailing
{

    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);
    }
}
