using Application.Features.Patients.Rules;
using Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Queries.GetById
{
    public class GetByIdPatientQuery : IRequest<GetByIdPatientResponse> 
    {
        public int Id { get; set; }
        public class GetByIdPatientQueryHandler : IRequestHandler<GetByIdPatientQuery, GetByIdPatientResponse>
        {
            private readonly IMapper _mapper;
            private readonly IPatientRepository _patientRepository;
            private readonly PatientBusinessRules _patientBusinessRules;

            public GetByIdPatientQueryHandler(IMapper mapper, IPatientRepository patientRepository, PatientBusinessRules patientBusinessRules)
            {
                _mapper = mapper;
                _patientRepository = patientRepository;
                _patientBusinessRules = patientBusinessRules;
            }

            public async Task<GetByIdPatientResponse> Handle(GetByIdPatientQuery request, CancellationToken cancellationToken)
            {
                var patient = await _patientRepository.GetAsync(
                    predicate: x => x.Id == request.Id,
                     include: query => query
                    .Include(d => d.Gender)
                    .Include(d => d.BloodType),
                    enableTracking: false
                    );

                _patientBusinessRules.IsSelectedEntityAvailable(patient);

                var response = _mapper.Map<GetByIdPatientResponse>( patient );
                return response;
                throw new NotImplementedException();
            }
        }
    }
}
