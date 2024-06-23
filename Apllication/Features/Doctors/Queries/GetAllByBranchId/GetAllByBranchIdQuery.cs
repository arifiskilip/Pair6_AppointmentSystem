using Application.Features.Doctors.Rules;
using Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Doctors.Queries.GetAllByBranchId
{
    public class GetAllByBranchIdQuery : IRequest<List<GetAllByBranchIdResponse>>
    {
        public short BranchId { get; set; }


        public class GetAllByBranchIdQueryHandler : IRequestHandler<GetAllByBranchIdQuery, List<GetAllByBranchIdResponse>>
        {
            private readonly IDoctorRepository _doctorRepository;
            private readonly DoctorBusinessRules _businessRules;
            private readonly IMapper _mapper;

            public GetAllByBranchIdQueryHandler(IDoctorRepository doctorRepository, DoctorBusinessRules businessRules, IMapper mapper)
            {
                _doctorRepository = doctorRepository;
                _businessRules = businessRules;
                _mapper = mapper;
            }

            public async Task<List<GetAllByBranchIdResponse>> Handle(GetAllByBranchIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _doctorRepository.GetListNotPagedAsync(
                    predicate: x => x.BranchId == request.BranchId,
                    include: i=> i
                    .Include(x=> x.Gender)
                    .Include(x=>x.Title)
                    .Include(x=> x.Branch),
                    orderBy: o => o.OrderBy(x => x.FirstName),
                    enableTracking: false);

                return _mapper.Map<List<GetAllByBranchIdResponse>>(result);
            }
        }
    }
}
