using Application.Features.Doctors.Constants;
using Application.Features.Titles.Constants;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
