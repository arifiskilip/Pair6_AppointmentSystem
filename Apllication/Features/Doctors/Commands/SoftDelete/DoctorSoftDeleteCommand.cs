using Application.Features.Doctors.Rules;
using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Doctors.Commands.SoftDelete
{
    public class DoctorSoftDeleteCommand : IRequest<DoctorSoftDeleteResponse>, ISecuredRequest
    {
        public int DoctorId { get; set; }
        public string[] Roles => ["Admin"];


        public class DoctorSoftDeleteCommandHandler : IRequestHandler<DoctorSoftDeleteCommand, DoctorSoftDeleteResponse>
        {
            private readonly IDoctorRepository _doctorRepository;
            private readonly DoctorBusinessRules _businessRules;
            private readonly IMapper _mapper;

            public DoctorSoftDeleteCommandHandler(IDoctorRepository doctorRepository, DoctorBusinessRules businessRules, IMapper mapper)
            {
                _doctorRepository = doctorRepository;
                _businessRules = businessRules;
                _mapper = mapper;
            }

            public async Task<DoctorSoftDeleteResponse> Handle(DoctorSoftDeleteCommand request, CancellationToken cancellationToken)
            {
                var checkDoctor = await _doctorRepository.GetAsync(
                    predicate: x => x.Id == request.DoctorId,
                    enableTracking: true);

                _businessRules.IsSelectedEntityAvailable(checkDoctor);


                checkDoctor.IsDeleted = true;
                await _doctorRepository.UpdateAsync(checkDoctor);
                return new();
            }
        }
    }
}
