using Application.Features.Auth.Constant;
using Application.Features.Auth.Rules;
using Application.Repositories;
using AutoMapper;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.Security.JWT;
using Core.Utilities.EncryptionHelper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Command.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
        {
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly ITokenHelper _tokenHelper;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;

            public LoginCommandHandler(AuthBusinessRules authBusinessRules, ITokenHelper tokenHelper, IMapper mapper, IUserRepository userRepository)
            {
                _authBusinessRules = authBusinessRules;
                _tokenHelper = tokenHelper;
                _mapper = mapper;
                _userRepository = userRepository;
            }

            public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                //Rules
                User user = await _authBusinessRules.UserEmailCheck(request.Email);
                _authBusinessRules.IsPasswordCorrectWhenLogin(user, request.Password);

               if(user.IsDeleted == true)
                {
                    throw new BusinessException("Giriş Başarısız");
                }

                IList<Core.Security.Entitites.OperationClaim> operationClaims = await _userRepository.GetClaimsAsync(user);
                AccessToken? accessToken = _tokenHelper.CreateToken(user, operationClaims);
                LoginResponse response = _mapper.Map<LoginResponse>(user);
                response.AccessToken = accessToken;
                return response;
            }
        }
    }
}
