using Application.Features.Auth.Constant;
using Application.Features.Auth.Rules;
using Application.Helpers;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Mailing;
using Core.Mailing.Constant;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Command.VerificationCode
{
    public class VerificationCodeCommand : IRequest<VerificationCodeResponse>, ISecuredRequest
    {
        public string[] Roles => [];

        public class VerificationCodeHandler : IRequestHandler<VerificationCodeCommand, VerificationCodeResponse>
        {
            private readonly IAuthService _authService;
            private readonly IMapper _mapper;
            private readonly IEmailService _emailService;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IVerificationCodeRepository _verificationCodeRepository;

            public VerificationCodeHandler(IAuthService authService, IMapper mapper, IEmailService emailService, AuthBusinessRules authBusinessRules, IVerificationCodeRepository verificationCodeRepository)
            {
                _authService = authService;
                _mapper = mapper;
                _emailService = emailService;
                _authBusinessRules = authBusinessRules;
                _verificationCodeRepository = verificationCodeRepository;
            }

            public async Task<VerificationCodeResponse> Handle(VerificationCodeCommand request, CancellationToken cancellationToken)
            {
                int userId = await _authService.GetAuthenticatedUserIdAsync();
                int codeTypeId = (int)CodeTypeEnum.EmailConfirm;
                await _authBusinessRules.CheckUserByIdAsync(userId:userId);
                string userEmail = await _authBusinessRules.GetUserEmailAsync(userId:userId);
                var check = await _verificationCodeRepository.GetAsync(
                    predicate: x => x.UserId == userId && x.CodeTypeId == codeTypeId);
                string code = VerificationCodeHelper.GenerateVerificationCode();
                if (check is null)
                {
                    var verificationCode = await _verificationCodeRepository.AddAsync(new()
                    {
                        CodeTypeId = codeTypeId,
                        UserId = userId,
                        ExpirationDate = DateTime.Now.AddMinutes(1),
                        Code = code
                    });
                    await _emailService.SendEmailAsync(userEmail, AuthMessages.VerificationCode, HtmlBody.OtpVerified(code));
                    return _mapper.Map<VerificationCodeResponse>(verificationCode);
                }
                else
                {
                    check.Code = code;
                    check.ExpirationDate = DateTime.Now.AddMinutes(1);
                    await _emailService.SendEmailAsync(userEmail, AuthMessages.VerificationCode, HtmlBody.OtpVerified(code));
                    await _verificationCodeRepository.UpdateAsync(check);
                }
                return _mapper.Map<VerificationCodeResponse>(check);
            }
            
        }
    }
}
