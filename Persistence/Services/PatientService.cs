using Application.Repositories;
using Application.Services;
using Domain.Entities;

namespace Persistence.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<bool> IsPatientAvailableAsync(int patientId)
        {
            Patient? checkPatient = await _patientRepository.GetAsync(x => x.Id == patientId);
            return checkPatient is not null ? 
                true : false;
        }
    }
}
