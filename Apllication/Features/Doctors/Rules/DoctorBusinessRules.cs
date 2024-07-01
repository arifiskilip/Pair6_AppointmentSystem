using Application.Features.Doctors.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Doctors.Rules
{
    public class DoctorBusinessRules : BaseBusinessRules
    {
        public void IsSelectedEntityAvailable(Doctor? doctor)
        {
            if (doctor == null) throw new BusinessException(DoctorMessages.DoctorNotAvailable);
        }
    }
}
