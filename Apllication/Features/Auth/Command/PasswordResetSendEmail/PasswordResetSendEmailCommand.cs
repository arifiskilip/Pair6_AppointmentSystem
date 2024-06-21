using Application.Features.Auth.Command.VerificationCode;
using Application.Features.Auth.Constant;
using Application.Features.Auth.Rules;
using Application.Helpers;
using Application.Repositories;
using Application.Services;
using Core.Mailing;
using Core.Mailing.Constant;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Command.PasswordResetSendEmail
{
    public class PasswordResetSendEmailCommand : IRequest<PasswordResetSendEmailResponse>
    {
        public string Email { get; set; }



        public class PasswordResetSendEmailHandler : IRequestHandler<PasswordResetSendEmailCommand, PasswordResetSendEmailResponse>
        {
            private readonly IUserService _userService;
            private readonly IVerificationCodeRepository _verificationCodeRepository;
            private readonly AuthBusinessRules _businessRules;
            private readonly IEmailService _emailService;

            public PasswordResetSendEmailHandler(IUserService userService, IVerificationCodeRepository verificationCodeRepository, AuthBusinessRules businessRules, IEmailService emailService)
            {
                _userService = userService;
                _verificationCodeRepository = verificationCodeRepository;
                _businessRules = businessRules;
                _emailService = emailService;
            }

            public async Task<PasswordResetSendEmailResponse> Handle(PasswordResetSendEmailCommand request, CancellationToken cancellationToken)
            {
                var checkUser = await _userService.GetUserByEmailAsync(request.Email);

                _businessRules.IsSelectedEntityAvailable(checkUser);

                var check = await _verificationCodeRepository.GetAsync(
                predicate: x => x.UserId == checkUser.Id && x.CodeTypeId == (int)CodeTypeEnum.PasswordReset);
                string code = VerificationCodeHelper.GenerateVerificationCode();

                if (check is null)
                {
                    var verificationCode = await _verificationCodeRepository.AddAsync(new()
                    {
                        CodeTypeId = (int)CodeTypeEnum.PasswordReset,
                        UserId = checkUser.Id,
                        ExpirationDate = DateTime.Now.AddMinutes(3),
                        Code = code
                    });
                    await _emailService.SendEmailAsync(request.Email, AuthMessages.PasswordReset, HtmlBody.PasswordReset(code));
                }

                else
                {
                    check.Code = code;
                    check.ExpirationDate = DateTime.Now.AddMinutes(3);
                    await _emailService.SendEmailAsync(request.Email, AuthMessages.PasswordReset, HtmlBody.PasswordReset(code));
                    await _verificationCodeRepository.UpdateAsync(check);
                }

                return new()
                {
                    Email = request.Email
                };
            }
        }
    }
}
