using Application.Features.Auth.Rules;
using Application.Repositories;
using AutoMapper;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;

            public LoginCommandHandler(AuthBusinessRules authBusinessRules, ITokenHelper tokenHelper, IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository)
            {
                _authBusinessRules = authBusinessRules;
                _tokenHelper = tokenHelper;
                _mapper = mapper;
                _userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var user = await _authBusinessRules.UserEmailCheck(request.Email);
                _authBusinessRules.IsPasswordCorrectWhenLogin(user, request.Password);

               

                //List<UserOperationClaim> userOperationClaims = query.ToList();

                IQueryable<UserOperationClaim> query = await _userOperationClaimRepository
                  .GetListNotPagedAsync(i => i.UserId == user.Id, include: i => i.Include(i => i.OperationClaim), enableTracking: false);

                List<OperationClaim> userOperationClaims = query.Select(i => i.OperationClaim).ToList();

                // Domain.Entities.OperationClaim nesnelerini Core.Security.Entities.OperationClaim nesnelerine dönüştürün
                var operationClaims = userOperationClaims
                    .Select(i => new Core.Security.Entitites.OperationClaim
                    {
                        Id = i.Id,
                        Name = i.Name
                    })
                    .ToList();

                // Token oluşturun
                AccessToken token = _tokenHelper.CreateToken(user, operationClaims);

                // LoginResponse nesnesi oluşturun ve geri döndürün
                var loginResponse = new LoginResponse
                {
                    Token = token.Token,
                    Expiration = token.Expiration,
                    UserName = $"{user.FirstName} {user.LastName}"
                };

                return loginResponse;
            }
        }
    }
}
