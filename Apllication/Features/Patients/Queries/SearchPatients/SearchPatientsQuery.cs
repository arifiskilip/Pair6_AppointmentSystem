using Application.Features.Patients.Queries.GetAllPaginated;
using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Ocsp;
using System.Linq.Expressions;

namespace Application.Features.Patients.Queries.SearchPatients
{
    public class SearchPatientsQuery : IRequest<IPaginate<SearchPatientsResponse>>, ISecuredRequest
    {
        public string? SearchTerm { get; set; }

        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;

        public string[] Roles => ["Doctor", "Admin"];

        public class SearchPatientsQueryHandler : IRequestHandler<SearchPatientsQuery, IPaginate<SearchPatientsResponse>>
        {
            private readonly IPatientRepository _patientRepository;
            private readonly IMapper _mapper;

            public SearchPatientsQueryHandler(IPatientRepository patientRepository, IMapper mapper)
            {
                _patientRepository = patientRepository;
                _mapper = mapper;
            }

            public async Task<IPaginate<SearchPatientsResponse>> Handle(SearchPatientsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Patient, bool>> query = null;
                if (!string.IsNullOrEmpty(request.SearchTerm))
                {
                    query = x =>
                 x.FirstName.ToLower().Contains(request.SearchTerm.ToLower()) ||
                 x.LastName.ToLower().Contains(request.SearchTerm.ToLower()) ||
                 x.IdentityNumber.ToLower().Contains(request.SearchTerm.ToLower()) ||
                 x.Email.ToLower().Contains(request.SearchTerm.ToLower()) ||
                 x.PhoneNumber.ToLower().Contains(request.SearchTerm.ToLower());
                }
                var patients = await _patientRepository.GetListAsync(
                 predicate: query,
                 index: request.Index,
                 size: request.Size,
                 include: x=> x.Include(i=> i.Gender).Include(i=> i.BloodType),
                 orderBy: x=> x.OrderBy(o=> o.FirstName),
                 enableTracking: false);

                var response = _mapper.Map<List<SearchPatientsResponse>>(patients.Items);

                return new Paginate<SearchPatientsResponse>(response.AsQueryable(), patients.Pagination);

            }
        }
    }
}
