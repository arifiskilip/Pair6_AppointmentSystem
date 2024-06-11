using Application.Services;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;

namespace Application.Features.Appointment.Rules
{
    public class AppointmentBusinessRules : BaseBusinessRules
    {
        private readonly IAppointmentIntervalService _appointmentIntervalService;

        public AppointmentBusinessRules(IAppointmentIntervalService appointmentIntervalService)
        {
            _appointmentIntervalService = appointmentIntervalService;
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

        }
    }
}
