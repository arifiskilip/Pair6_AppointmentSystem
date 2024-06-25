using Application.Features.Auth.Constant;
using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Command.EmailVerified
{
    public class EmailVerifiedCommand : IRequest<EmailVerifiedResponse>, ISecuredRequest
    {
        public string Code { get; set; }

        public string[] Roles => ["Patient", "Admin", "Doctor"];

        public class EmailVerifiedHandler : IRequestHandler<EmailVerifiedCommand, EmailVerifiedResponse>
        {
            private readonly IAuthService _authService;
            private readonly IUserService _userService;
            private readonly IVerificationCodeRepository _verificationCodeRepository;
            private readonly AuthBusinessRules _rules;

            public EmailVerifiedHandler(IVerificationCodeRepository verificationCodeRepository, AuthBusinessRules rules, IAuthService authService, IUserService userService)
            {
                _verificationCodeRepository = verificationCodeRepository;
                _rules = rules;
                _authService = authService;
                _userService = userService;
            }

            public async Task<EmailVerifiedResponse> Handle(EmailVerifiedCommand request, CancellationToken cancellationToken)
            {
                int userId = await _authService.GetAuthenticatedUserIdAsync();
                await _rules.CheckUserByIdAsync(userId);
                var verificationCode = await _verificationCodeRepository.GetAsync(x => x.UserId == userId && x.CodeTypeId == (int)CodeTypeEnum.EmailConfirm);
                if (!(verificationCode.ExpirationDate >= DateTime.Now))
                {
                    throw new BusinessException(AuthMessages.VerificationCodeTimeout);
                }
                if (verificationCode.Code != request.Code)
                {
                    throw new BusinessException(AuthMessages.IncorrectVerificationCode);
                }
                await _userService.SetUserEmailVerified(userId:userId);
                return new()
                {
                    Message = AuthMessages.SuccessVerificationCode
                };
            }
        }
    }
}
