using Application.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;

namespace Application.Features.AppointmentInterval.Rules
{
    public class AppointmentIntervalBusinessRules : BaseBusinessRules
    {
        private readonly IAppointmentIntervalRepository _appointmentIntervalRepository;

        public AppointmentIntervalBusinessRules(IAppointmentIntervalRepository appointmentIntervalRepository)
        {
            _appointmentIntervalRepository = appointmentIntervalRepository;
        }

        public void CheckBrancId(int branchId)
        {
            if (branchId <= 0)
            {
                throw new BusinessException("Branş alanı zorunlu");
            }
        }
    }
}
