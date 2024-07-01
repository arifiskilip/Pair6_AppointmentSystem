using Application.Features.Auth.Constant;
using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.Mailing;
using Core.Mailing.Constant;
using Core.Security.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Command.ResetPasswordByAdmin
{
    public class ResetPasswordByAdminCommand : IRequest<ResetPasswordByAdminResponse>, ISecuredRequest
    {
        public int UserId { get; set; }
        public string[] Roles => ["Admin"];

        public class ResetPasswordByAdminCommandHandler : IRequestHandler<ResetPasswordByAdminCommand, ResetPasswordByAdminResponse>
        {
            private readonly IEmailService _emailService;
            private readonly IUserRepository _userRepository;
            private readonly AuthBusinessRules _rules;

            public ResetPasswordByAdminCommandHandler(IEmailService emailService, AuthBusinessRules rules, IUserRepository userRepository)
            {
                _emailService = emailService;
                _rules = rules;
                _userRepository = userRepository;
            }

            public async Task<ResetPasswordByAdminResponse> Handle(ResetPasswordByAdminCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(predicate: x => x.Id == request.UserId);
                if (user == null)
                {
                    throw new BusinessException("Böyle bir kullanıcı bulunamadı.");
                }
                byte[] passwordHash, passwordSalt;
                string newPassword = RandomPasswordGenerator();
                HashingHelper.CreatePasswordHash(
                    password: newPassword,
                    passwordHash: out passwordHash,
                    passwordSalt: out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                await _userRepository.UpdateAsync(user);

                await _emailService.SendEmailAsync(
                    toEmail: user.Email,
                    subject: AuthMessages.PasswordReset,
                    body: HtmlBody.PasswordResetByAdmin(newPassword));

                
                return new()
                {
                    Message ="Yeni şifre mail adresine gönderildi"
                };
                
            }

            private static string RandomPasswordGenerator()
            {
                Random random = new();
                const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
                const string digitChars = "0123456789";
                const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

                // Ensure we have at least one character of each type
                var password = new StringBuilder();
                password.Append(upperChars[random.Next(upperChars.Length)]);
                password.Append(lowerChars[random.Next(lowerChars.Length)]);
                password.Append(digitChars[random.Next(digitChars.Length)]);
                password.Append(specialChars[random.Next(specialChars.Length)]);

                // Fill the remaining characters with a mix of all types
                const string allChars = upperChars + lowerChars + digitChars + specialChars;
                for (int i = password.Length; i < 6; i++)
                {
                    password.Append(allChars[random.Next(allChars.Length)]);
                }

                // Shuffle the password to ensure randomness
                return new string(password.ToString().OrderBy(c => random.Next()).ToArray());
            }
        }
    }
}
