using Application.Features.Reports.Queries.GetPaginatedReportsByPatientId;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reports.Queries.GetPaginatedReportsByPatientIdAndDoctorId
{
    public class GetPaginatedReportsByPatientIdAndDoctorIdQuery : IRequest<IPaginate<GetPaginatedReportsByPatientIdAndDoctorIdResponse>>, ISecuredRequest
    {
        public int PatientId { get; set; }

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string[] Roles => ["Doctor"];



        public class GetPaginatedReportsByPatientIdAndDoctorIdQueryHandler : IRequestHandler<GetPaginatedReportsByPatientIdAndDoctorIdQuery, IPaginate<GetPaginatedReportsByPatientIdAndDoctorIdResponse>>
        {
            private readonly IMapper _mapper;
            private readonly IReportRepository _reportRepository;
            private readonly IAuthService _authService;

            public GetPaginatedReportsByPatientIdAndDoctorIdQueryHandler(IMapper mapper, IReportRepository reportRepository, IAuthService authService)
            {
                _mapper = mapper;
                _reportRepository = reportRepository;
                _authService = authService;
            }

            public async Task<IPaginate<GetPaginatedReportsByPatientIdAndDoctorIdResponse>> Handle(GetPaginatedReportsByPatientIdAndDoctorIdQuery request, CancellationToken cancellationToken)
            {
                var doctorId = await _authService.GetAuthenticatedUserIdAsync();
                var reports = await _reportRepository.GetListAsync(
               predicate: r => r.Appointment.PatientId == request.PatientId && r.Appointment.AppointmentInterval.DoctorId == doctorId,
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

                var result = _mapper.Map<List<GetPaginatedReportsByPatientIdAndDoctorIdResponse>>(reports.Items);
                return new Paginate<GetPaginatedReportsByPatientIdAndDoctorIdResponse>(result.AsQueryable(), reports.Pagination);
            }
        }
    }
}
