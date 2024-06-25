using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reports.Queries.GetAllReportsPatient
{
    public class GetAllReportsPatientCommand : IRequest<GetAllReportsPatientResponse>, ISecuredRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string[] Roles => ["Patient"];

        public class GetAllReportsPatientCommandHandler : IRequestHandler<GetAllReportsPatientCommand, GetAllReportsPatientResponse>
        {
            private readonly IMapper _mapper;
            private readonly IReportRepository _reportRepository;
            private readonly IAuthService _authService;

            public GetAllReportsPatientCommandHandler(IMapper mapper, IReportRepository reportRepository, IAuthService authService)
            {
                _mapper = mapper;
                _reportRepository = reportRepository;
                _authService = authService;
            }

            public async Task<GetAllReportsPatientResponse> Handle(GetAllReportsPatientCommand request, CancellationToken cancellationToken)
            {

                //rules bi dursun
                var patientId = await _authService.GetAuthenticatedUserIdAsync();
               

                var reports = await _reportRepository.GetListAsync(
                predicate: r => r.Appointment.PatientId == patientId,
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

                var reportDtos = _mapper.Map<List<PatientReportsDto>>(reports.Items);

                return new GetAllReportsPatientResponse
                {
                    PatientReports = new Paginate<PatientReportsDto>(reportDtos.AsQueryable(), reports.Pagination)
                };

              
            }
        }
    }
}
