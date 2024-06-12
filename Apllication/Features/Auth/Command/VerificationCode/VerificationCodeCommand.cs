using Application.Features.Auth.Constant;
using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Mailing;
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
                string code = string.Empty;
                if (check is null)
                {
                    code = GenerateVerificationCode();
                    var verificationCode = await _verificationCodeRepository.AddAsync(new()
                    {
                        CodeTypeId = codeTypeId,
                        UserId = userId,
                        ExpirationDate = DateTime.Now.AddMinutes(1),
                        Code = code
                    });
                    await _emailService.SendEmailAsync(userEmail, AuthMessages.VerificationCode, code);
                    return _mapper.Map<VerificationCodeResponse>(verificationCode);
                }

                code = GenerateVerificationCode();
                check.Code = code;
                check.ExpirationDate = DateTime.Now.AddMinutes(1);
                await _emailService.SendEmailAsync(userEmail, AuthMessages.VerificationCode, code);
                await _verificationCodeRepository.UpdateAsync(check);
                return _mapper.Map<VerificationCodeResponse>(check);
            }
            private string GenerateVerificationCode()
            {
                Random random = new Random();
                const string chars = "0123456789";
                return new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }
    }
}
