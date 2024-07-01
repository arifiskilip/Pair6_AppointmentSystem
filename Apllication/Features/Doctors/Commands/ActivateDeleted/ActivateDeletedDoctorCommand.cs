using Application.Features.Doctors.Rules;
using Application.Repositories;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.Doctors.Commands.ActivateDeleted
{
    public class ActivateDeletedDoctorCommand : IRequest<ActivateDeletedDoctorResponse>, ISecuredRequest
    {
        public int DoctorId { get; set; }
        public string[] Roles => ["Admin"];


        public class ActivateDeletedDoctorCommandHandler : IRequestHandler<ActivateDeletedDoctorCommand, ActivateDeletedDoctorResponse>
        {
            private IDoctorRepository _doctorRepository;
            private DoctorBusinessRules _businessRules;

            public ActivateDeletedDoctorCommandHandler(IDoctorRepository doctorRepository, DoctorBusinessRules businessRules)
            {
                _doctorRepository = doctorRepository;
                _businessRules = businessRules;
            }

            public async Task<ActivateDeletedDoctorResponse> Handle(ActivateDeletedDoctorCommand request, CancellationToken cancellationToken)
            {
                var checkDoctor = await _doctorRepository.GetAsync(
                    predicate: x => x.Id == request.DoctorId,
                    enableTracking: true);

                _businessRules.IsSelectedEntityAvailable(checkDoctor);


                checkDoctor.IsDeleted = false;
                await _doctorRepository.UpdateAsync(checkDoctor);
                return new();
            }
        }
    }
}
