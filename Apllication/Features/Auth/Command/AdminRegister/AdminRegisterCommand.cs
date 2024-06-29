using Application.Features.Auth.Command.DoctorRegister;
using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Command.AdminRegister
{
    public class AdminRegisterCommand : IRequest<AdminRegisterResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string? IdentityNumber { get; set; }
        public short GenderId { get; set; }
        public bool IsEmailVerified { get; set; } = true;

        public class AdminRegisterCommandHandler : IRequestHandler<AdminRegisterCommand, AdminRegisterResponse>
        {
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;
            private readonly IUserOperationClaimService _userOperationClaimService;

            public AdminRegisterCommandHandler(ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules, IMapper mapper, IUserRepository userRepository, IUserOperationClaimService userOperationClaimService)
            {
                _tokenHelper = tokenHelper;
                _authBusinessRules = authBusinessRules;
                _mapper = mapper;
                _userRepository = userRepository;
                _userOperationClaimService = userOperationClaimService;
            }

            public async Task<AdminRegisterResponse> Handle(AdminRegisterCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.DuplicateEmailChechAsync(request.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(
                    password: request.Password,
                    passwordHash: out passwordHash,
                    passwordSalt: out passwordSalt);

                var admin = _mapper.Map<User>(request);
                admin.PasswordHash = passwordHash;
                admin.PasswordSalt = passwordSalt;
                var result = await _userRepository.AddAsync(admin);
                await _userOperationClaimService.AddAsync(new()
                {
                    UserId = admin.Id,
                    OperationClaimId = (int)OperationClaimEnum.Admin
                });
                return _mapper.Map<AdminRegisterResponse>(admin);
                throw new NotImplementedException();
            }
        }
    }
}
