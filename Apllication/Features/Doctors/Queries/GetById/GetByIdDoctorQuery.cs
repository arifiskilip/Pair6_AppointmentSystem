using Application.Features.Doctors.Rules;
using Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Queries.GetById
{
    public class GetByIdDoctorQuery : IRequest<GetByIdDoctorResponse>
    {
        public int Id { get; set; }
        public class GetByIdDoctorQueryHandler : IRequestHandler<GetByIdDoctorQuery, GetByIdDoctorResponse>
        {
            private readonly IMapper _mapper;
            private readonly IDoctorRepository _doctorRepository;
            private readonly DoctorBusinessRules _doctorBusinessRules;

            public GetByIdDoctorQueryHandler(IMapper mapper, IDoctorRepository doctorRepository, DoctorBusinessRules doctorBusinessRules)
            {
                _mapper = mapper;
                _doctorRepository = doctorRepository;
                _doctorBusinessRules = doctorBusinessRules;

            }

            public async Task<GetByIdDoctorResponse> Handle(GetByIdDoctorQuery request, CancellationToken cancellationToken)
            {
                var doctor = await _doctorRepository.GetAsync(
                    predicate: x => x.Id == request.Id,
                    include: query => query
                    .Include(d => d.Title)
                    .Include(d => d.Branch),
                    enableTracking: false);

                 _doctorBusinessRules.IsSelectedEntityAvailable(doctor);

                var response = _mapper.Map<GetByIdDoctorResponse>(doctor);
                return response;
            }
        }
    }
}
