using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.Reports.Queries.GetPaginatedFilteredReportsByPatientId
{
    public class GetPaginatedFilteredReportsByDoctorIdQuery : IRequest<IPaginate<GetPaginatedFilteredReportsByDoctorIdResponse>>
    {
        public int DoctorId { get; set; }
        public string? OrderBy { get; set; }
        public DateTime? Date { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

       // public string[] Roles => ["Doctor","Admin"];
    }


    public class GetPaginatedFilteredReportsByDoctorIdQueryHandler : IRequestHandler<GetPaginatedFilteredReportsByDoctorIdQuery, IPaginate<GetPaginatedFilteredReportsByDoctorIdResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;

        public GetPaginatedFilteredReportsByDoctorIdQueryHandler(IMapper mapper, IReportRepository reportRepository)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
        }

        public async Task<IPaginate<GetPaginatedFilteredReportsByDoctorIdResponse>> Handle(GetPaginatedFilteredReportsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _reportRepository.GetPaginatedFilteredReportsByPatientI(
                request.DoctorId, request.OrderBy, request.Date, request.PageIndex, request.PageSize);

            var mappingData = _mapper.Map<List<GetPaginatedFilteredReportsByDoctorIdResponse>>(result.Items);
            return new Paginate<GetPaginatedFilteredReportsByDoctorIdResponse>(mappingData.AsQueryable(), result.Pagination);  
            
        }
    }
}
