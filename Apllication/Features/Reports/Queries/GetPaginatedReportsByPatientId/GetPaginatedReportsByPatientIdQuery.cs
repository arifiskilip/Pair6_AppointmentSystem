using Application.Features.Reports.Queries.GetAllReportsPatient;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reports.Queries.GetPaginatedReportsByPatientId
{
    public class GetPaginatedReportsByPatientIdQuery : IRequest<IPaginate<GetPaginatedReportsByPatientIdResonse>>, ISecuredRequest
    {
        public int PatientId { get; set; }

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string[] Roles => ["Doctor"];


        public class GetPaginatedReportsByPatientIdResonseHandler : IRequestHandler<GetPaginatedReportsByPatientIdQuery, IPaginate<GetPaginatedReportsByPatientIdResonse>>
        {
            private readonly IMapper _mapper;
            private readonly IReportRepository _reportRepository;

            public GetPaginatedReportsByPatientIdResonseHandler(IMapper mapper, IReportRepository reportRepository)
            {
                _mapper = mapper;
                _reportRepository = reportRepository;
            }

            public async Task<IPaginate<GetPaginatedReportsByPatientIdResonse>> Handle(GetPaginatedReportsByPatientIdQuery request, CancellationToken cancellationToken)
            {

                var reports = await _reportRepository.GetListAsync(
                predicate: r => r.Appointment.PatientId == request.PatientId,
                include: query => query
                    .Include(r => r.Appointment)
                            .ThenInclude(a => a.Patient)
                    .Include(r => r.Appointment)
                        .ThenInclude(a => a.AppointmentInterval)
                            .ThenInclude(ai => ai.Doctor)
                             .ThenInclude(d => d.Branch)
                    .Include(r => r.Appointment)
                        .ThenInclude(a => a.AppointmentInterval)
                            .ThenInclude(ai => ai.Doctor)
                                .ThenInclude(d => d.Title)
                    .Include(r => r.Appointment)
                        .ThenInclude(a => a.AppointmentStatus),
                orderBy: q => q.OrderByDescending(r => r.Appointment.AppointmentInterval.IntervalDate),
                index: request.PageIndex,
                size: request.PageSize,
                enableTracking: false,
                cancellationToken: cancellationToken
                );

                var result = _mapper.Map<List<GetPaginatedReportsByPatientIdResonse>>(reports.Items);
                return new Paginate<GetPaginatedReportsByPatientIdResonse>(result.AsQueryable(), reports.Pagination);
            }
        }
    }
}
