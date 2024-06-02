using Application.Features.Auth.Command.PatientRegister;
using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Command.DoctorRegister
{
    public class DoctorRegisterCommand : IRequest<DoctorRegisterResponse>
    {
        public short TitleId { get; set; }
        public short BranchId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string? IdentityNumber { get; set; }

        public class DoctorRegisterHandler : IRequestHandler<DoctorRegisterCommand, DoctorRegisterResponse>
        {
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IMapper _mapper;
            private readonly IDoctorRepository _doctorRepository;
            private readonly IUserOperationClaimService _userOperationClaimService;

            public DoctorRegisterHandler(ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules, IMapper mapper, IDoctorRepository doctorRepository, IUserOperationClaimService userOperationClaimService)
            {
                _tokenHelper = tokenHelper;
                _authBusinessRules = authBusinessRules;
                _mapper = mapper;
                _doctorRepository = doctorRepository;
                _userOperationClaimService = userOperationClaimService;
            }

            async Task<DoctorRegisterResponse> IRequestHandler<DoctorRegisterCommand, DoctorRegisterResponse>.Handle(DoctorRegisterCommand request, CancellationToken cancellationToken)
            {
                //Rules
                await _authBusinessRules.DuplicateEmailChechAsync(request.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(
                    password: request.Password,
                    passwordHash: out passwordHash,
                    passwordSalt: out passwordSalt);
                Doctor? doctor = _mapper.Map<Doctor>(request);
                doctor.PasswordHash = passwordHash;
                doctor.PasswordSalt = passwordSalt;
                var result = await _doctorRepository.AddAsync(doctor);
                await _userOperationClaimService.AddAsync(new()
                {
                    UserId = doctor.Id,
                    OperationClaimId = (int)OperationClaimEnum.Doctor
                });

                return _mapper.Map<DoctorRegisterResponse>(doctor);
            }
        }
    }
}
