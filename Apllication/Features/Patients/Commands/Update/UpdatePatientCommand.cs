using Application.Features.Patients.Rules;
using Application.Repositories;
using AutoMapper;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Commands.Update
{
    public class UpdatePatientCommand :IRequest<UpdatePatientResponse>
    {
        public int Id { get; set; } = 3; // Şu an için Id'yi 3 olarak sabitliyoruz
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string BloodType { get; set; }
        public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, UpdatePatientResponse>
        {
            private readonly IPatientRepository _patientRepository;
            private readonly IMapper _mapper;
            private readonly PatientBusinessRules _patientBusinessRules;

            public UpdatePatientCommandHandler(IPatientRepository patientRepository, IMapper mapper, PatientBusinessRules patientBusinessRules)
            {
                _patientRepository = patientRepository;
                _mapper = mapper;
                _patientBusinessRules = patientBusinessRules;
            }

            public async Task<UpdatePatientResponse> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
            {
                //var checkPatient = await _patientRepository.GetAsync(
                //predicate: x => x.Id == request.Id,
                //enableTracking:false
                //);

                //await _patientBusinessRules.DuplicateMailCheckAsync(request.Email);

                //var patient =_mapper.Map<Patient>(request);

                //var updatedPatient = await _patientRepository.UpdateAsync(patient);

                //return _mapper.Map<UpdatePatientResponse>(updatedPatient);

                var existingPatient = await _patientRepository.GetAsync(
                predicate: x => x.Id == request.Id,
                enableTracking: false
                );
                // Duplicate mail check
                await _patientBusinessRules.DuplicateMailCheckAsync(request.Email);

                // Mapper kullanarak sadece gerekli alanları kopyalayın
                var updatedPatient = _mapper.Map(request, existingPatient);

                // passwordHash ve passwordSalt alanlarının üzerine yazılmasını engelleyin
                updatedPatient.PasswordHash = existingPatient.PasswordHash;
                updatedPatient.PasswordSalt = existingPatient.PasswordSalt;

                // Güncellenmiş hastayı veritabanına kaydedin
                await _patientRepository.UpdateAsync(updatedPatient);

                return _mapper.Map<UpdatePatientResponse>(updatedPatient);


            }
        }
    }
}
