using Application.Features.Patients.Constants;
using Application.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.Utilities.EncryptionHelper;
using Domain.Entities;
using System.Xml.Linq;

namespace Application.Features.Patients.Rules
{
    public class PatientBusinessRules:BaseBusinessRules
    {
        private readonly IPatientRepository _patientRepository;

        public PatientBusinessRules(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public void IsSelectedEntityAvailable(Patient? patient)
        {
            if (patient == null) throw new BusinessException(PatientMessages.PatientNotAvailable);
        }

        public async Task DuplicateMailCheckAsync(string mail)
        {
            var check = await _patientRepository
            .GetAsync(x => x.Email == EncryptionHelper.Encrypt(mail));
            if (check != null )
            {
                throw new BusinessException(PatientMessages.DuplicateEmailName);
            }
        }

        public async Task UpdateDuplicateNameCheckAsync(string email, int id)
        {
            var check = await _patientRepository.GetAsync(
                predicate: x => x.Email == EncryptionHelper.Encrypt(email));
            if (check != null && check.Id != id) throw new BusinessException(PatientMessages.DuplicateEmailName);
        }
    }
}
