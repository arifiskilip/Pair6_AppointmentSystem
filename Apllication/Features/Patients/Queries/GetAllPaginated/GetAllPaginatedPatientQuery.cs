using Application.Features.Doctors.Queries;
using Application.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Queries.GetAllPaginated
{
    public class GetAllPaginatedPatientQuery : IRequest<GetAllPaginatedPatientResponse>
    {

        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;

        public class GetAllPaginatedPatientQueryHandler : IRequestHandler<GetAllPaginatedPatientQuery, GetAllPaginatedPatientResponse>
        {

            private readonly IPatientRepository _patientRepository;
            private readonly IMapper _mapper;

            public GetAllPaginatedPatientQueryHandler(IPatientRepository patientRepository, IMapper mapper)
            {
                _patientRepository = patientRepository;
                _mapper = mapper;
            }

            public async Task<GetAllPaginatedPatientResponse> Handle(GetAllPaginatedPatientQuery request, CancellationToken cancellationToken)
            {
                var patients = await _patientRepository.GetListAsync(
                include: query => query
               .Include(p => p.BloodType)
               .Include(p => p.Gender),
                index: request.Index,
                size: request.Size,
                enableTracking: false,
                cancellationToken: cancellationToken
                );

                var patientDtos = _mapper.Map<List<ListPatientDto>>(patients.Items);
                return new GetAllPaginatedPatientResponse
                {
                    Patients = new Paginate<ListPatientDto>(patientDtos.AsQueryable(), patients.Pagination)
                };
            }
        }
    }
}
