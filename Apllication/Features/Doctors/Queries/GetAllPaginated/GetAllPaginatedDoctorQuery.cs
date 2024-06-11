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

namespace Application.Features.Doctors.Queries.GetAllPaginated
{
    public class GetAllPaginatedDoctorQuery : IRequest<GetAllPaginatedDoctorResponse>
    {
        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;
        public class GetAllPaginatedDoctorQueryHandler : IRequestHandler<GetAllPaginatedDoctorQuery, GetAllPaginatedDoctorResponse>
        {
            private readonly IDoctorRepository _doctorRepository;
            private readonly IMapper _mapper;


            public GetAllPaginatedDoctorQueryHandler(IDoctorRepository doctorRepository, IMapper mapper)
            {
                _doctorRepository = doctorRepository;
                _mapper = mapper;

            }

            public async Task<GetAllPaginatedDoctorResponse> Handle(GetAllPaginatedDoctorQuery request, CancellationToken cancellationToken)
            {

                var doctors = await _doctorRepository.GetListAsync(
                include: query => query
               .Include(d => d.Title)
               .Include(d => d.Branch),
               index: request.Index,
               size: request.Size,
               enableTracking: false,
               cancellationToken: cancellationToken
            );
                


                var doctorDtos = _mapper.Map<List<ListDoctorDto>>(doctors.Items);

                return new GetAllPaginatedDoctorResponse
                {
                    Doctors = new Paginate<ListDoctorDto>(doctorDtos.AsQueryable(), doctors.Pagination)
                };
            }
        }
    }
}
