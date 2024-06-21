using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Command.PatientRegister
{
    public class PatientRegisterCommand : IRequest<PatientRegisterResponse>, ITransactionalRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdentityNumber { get; set; }
        public string Password { get; set; }
        public short BloodTypeId { get; set; }
        public short GenderId { get; set; }


        public class PatientRegisterHandler : IRequestHandler<PatientRegisterCommand, PatientRegisterResponse>
        {
            private readonly ITokenHelper _tokenHelper;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;
            private readonly IUserOperationClaimService _userOperationClaimService;

            public PatientRegisterHandler(ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules, IMapper mapper, IUserRepository userRepository, IUserOperationClaimService userOperationClaimService)
            {
                _tokenHelper = tokenHelper;
                _authBusinessRules = authBusinessRules;
                _mapper = mapper;
                _userRepository = userRepository;
                _userOperationClaimService = userOperationClaimService;
            }

            public async Task<PatientRegisterResponse> Handle(PatientRegisterCommand request, CancellationToken cancellationToken)
            {
                //Rules
                await _authBusinessRules.DuplicateEmailChechAsync(request.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(
                    password: request.Password,
                    passwordHash:out passwordHash,
                    passwordSalt:out passwordSalt);
                Patient? patient = _mapper.Map<Patient>(request);
                patient.PasswordHash = passwordHash;
                patient.PasswordSalt = passwordSalt;
                await _userRepository.AddAsync(patient);
                await _userOperationClaimService.AddAsync(new()
                {
                    UserId = patient.Id,
                    OperationClaimId = (int)OperationClaimEnum.Patient
                });

                return _mapper.Map<PatientRegisterResponse>(patient);
            }
        }
    }
}
