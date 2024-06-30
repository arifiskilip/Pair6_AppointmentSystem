using Application.Features.Patients.Rules;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Commands.DeleteByAdmin
{
    public class DeletePatientByAdminCommand : IRequest<DeletePatientByAdminResponse>
    {
        public int PatientId { get; set; }

        public class DeletePatientByAdminCommandHandler : IRequestHandler<DeletePatientByAdminCommand, DeletePatientByAdminResponse>
        {
            private readonly IPatientRepository _patientRepository;
            private readonly PatientBusinessRules _patientBusinessRules;

            public DeletePatientByAdminCommandHandler(IPatientRepository patientRepository, PatientBusinessRules patientBusinessRules)
            {
                _patientRepository = patientRepository;
                _patientBusinessRules = patientBusinessRules;
            }

            public async Task<DeletePatientByAdminResponse> Handle(DeletePatientByAdminCommand request, CancellationToken cancellationToken)
            {
               var patient =await  _patientRepository.GetAsync(predicate: x=> x.Id == request.PatientId);

                _patientBusinessRules.IsSelectedEntityAvailable(patient);

                patient.IsDeleted = !patient.IsDeleted;

                await _patientRepository.UpdateAsync(patient);
                return new();
            }
        }
    }
}
