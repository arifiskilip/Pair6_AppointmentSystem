using Application.Features.Auth.Rules;
using Application.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Features.Auth.Command.UpdatePassword
{
    public class UpdatePasswordCommand :IRequest<UpdatePasswordResponse> , ISecuredRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }

        public string[] Roles => ["Patient", "Doctor","Admin"];
        public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UpdatePasswordResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthBusinessRules _authBusinessRules;

            public UpdatePasswordCommandHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules)
            {
                _userRepository = userRepository;
                _httpContextAccessor = httpContextAccessor;
                _tokenHelper = tokenHelper;
                _authBusinessRules = authBusinessRules;
            }

            public async Task<UpdatePasswordResponse> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
            {

                // Kullanıcı ID'sini HttpContext'ten al
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                {
                    throw new BusinessException("Kullanıcı kimliği bulunamadı.");
                }

                var user = await _userRepository.GetAsync(u => u.Id == int.Parse(userId));

               
                _authBusinessRules.IsSelectedEntityAvailable(user);
                _authBusinessRules.IsCurrentPasswordCorrect(user, request.CurrentPassword);
                _authBusinessRules.CheckNewPasswordsMatch(request.NewPassword, request.NewPasswordAgain);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.NewPassword, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _userRepository.UpdateAsync(user);

                return new UpdatePasswordResponse
                {
                    Message = "Şifre başarıyla güncellendi."
                };
            }
        }
    }
}
