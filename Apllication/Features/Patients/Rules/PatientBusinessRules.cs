using Application.Features.Branchs.Constants;
using Application.Features.Doctors.Constants;
using Application.Features.Patients.Constants;
using Application.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            .GetAsync(x => x.Email == mail);
            if (check != null )
            {
                throw new BusinessException(PatientMessages.DuplicateEmailName);
            }
        }
    }
}
