using Application.Services;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;

namespace Application.Features.Appointment.Rules
{
    public class AppointmentBusinessRules : BaseBusinessRules
    {
        private readonly IAppointmentIntervalService _appointmentIntervalService;
        private readonly IPatientService _patientService;

        public AppointmentBusinessRules(IAppointmentIntervalService appointmentIntervalService, IPatientService patientService)
        {
            _appointmentIntervalService = appointmentIntervalService;
            _patientService = patientService;
        }

        public async Task AppointmentIntervalAvailable(int appointmentIntervalId)
        {
            bool result = await _appointmentIntervalService.IsAppointmentAvailableAsync(appointmentIntervalId);
            if (!result)
            {
                throw new BusinessException("Bu randevu zaten oluşturulmuş. Lütfen farklı bir randevu aralığı seçiniz.");
            }
        }

        public async Task PatientAvailable(int patientId)
        {
            bool result = await _patientService.IsPatientAvailableAsync(patientId);
            if (!result)
            {
                throw new BusinessException("Sistemde böyle bir hasta mevcut değil.");
            }
        }
    }
}
