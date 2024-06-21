using Core.Mailing.Constant;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Core.Mailing
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IConfiguration configuration)
        {
            _emailSettings = configuration.GetSection("EmalSettings").Get<EmailSettings>();
        }
        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(name: _emailSettings.Name, address: _emailSettings.Email));
            message.To.Add(new MailboxAddress(toEmail, toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(host: _emailSettings.SmptpServer, _emailSettings.SmptpPort, useSsl: false);
                await client.AuthenticateAsync(userName: _emailSettings.Email, password: _emailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return true;
            }
        }
    }
}
