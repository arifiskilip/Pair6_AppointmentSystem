using Application.Features.Auth.Constant;
using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.Security.Hashing;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System.Text;
using System;
using Core.Mailing;
using Core.Mailing.Constant;

namespace Application.Features.Auth.Command.PasswordResetCodeVerified
{
    public class PasswordResetCodeVerifiedCommand : IRequest<PasswordResetCodeVerifiedResponse>
    {
        public string Code { get; set; }
        public string Email { get; set; }

        public class PasswordResetCodeVerifiedHandler : IRequestHandler<PasswordResetCodeVerifiedCommand, PasswordResetCodeVerifiedResponse>
        {
            private readonly IEmailService _emailService;
            private readonly IUserService _userService;
            private readonly IVerificationCodeRepository _verificationCodeRepository;
            private readonly AuthBusinessRules _rules;

            public PasswordResetCodeVerifiedHandler(IEmailService emailService, IUserService userService, IVerificationCodeRepository verificationCodeRepository, AuthBusinessRules rules)
            {
                _emailService = emailService;
                _userService = userService;
                _verificationCodeRepository = verificationCodeRepository;
                _rules = rules;
            }

            public async Task<PasswordResetCodeVerifiedResponse> Handle(PasswordResetCodeVerifiedCommand request, CancellationToken cancellationToken)
            {
                var checkUser = await _userService.GetUserByEmailAsync(request.Email);
                _rules.IsSelectedEntityAvailable(checkUser);

                var verificationCode = await _verificationCodeRepository.GetAsync(x => x.UserId == checkUser.Id && x.CodeTypeId == (int)CodeTypeEnum.PasswordReset);
                if (!(verificationCode.ExpirationDate >= DateTime.Now))
                {
                    throw new BusinessException(AuthMessages.VerificationCodeTimeout);
                }
                if (verificationCode.Code != request.Code)
                {
                    throw new BusinessException(AuthMessages.IncorrectVerificationCode);
                }

                byte[] passwordHash, passwordSalt;
                string newPassword = RandomPasswordGenerator();
                HashingHelper.CreatePasswordHash(
                    password: newPassword,
                    passwordHash: out passwordHash,
                    passwordSalt: out passwordSalt);
                checkUser.PasswordHash = passwordHash;
                checkUser.PasswordSalt = passwordSalt;
                await _userService.UpdateAsync(checkUser);

                await _emailService.SendEmailAsync(
                    toEmail:request.Email,
                    subject:AuthMessages.PasswordReset,
                    body:HtmlBody.NewPassword(newPassword));

                return new()
                {
                    Message = AuthMessages.SuccessVerificationCode
                };
            }
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
