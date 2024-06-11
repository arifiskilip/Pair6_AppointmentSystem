using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using Core.Security.Hashing;
using MediatR;

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
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IAuthService _authService;

            public UpdatePasswordCommandHandler(IUserRepository userRepository, AuthBusinessRules authBusinessRules, IAuthService authService)
            {
                _userRepository = userRepository;
                _authBusinessRules = authBusinessRules;
                _authService = authService;
            }

            public async Task<UpdatePasswordResponse> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
            {

                // Kullanıcı ID'sini HttpContext'ten al
                int userId = await _authService.GetAuthenticatedUserIdAsync();

                var user = await _userRepository.GetAsync(u => u.Id == userId);

               
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
