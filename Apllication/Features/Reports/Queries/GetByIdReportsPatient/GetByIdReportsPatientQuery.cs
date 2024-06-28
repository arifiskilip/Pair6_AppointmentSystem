using Application.Features.Reports.Rules;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reports.Queries.GetByIdReportsPatient
{
    public class GetByIdReportsPatientQuery : IRequest<GetByIdReportsPatientResponse>, ISecuredRequest
    {
        public int ReportId { get; set; }

        public string[] Roles => ["Patient"];

        public class GetByIdReportsPatientQueryHandler : IRequestHandler<GetByIdReportsPatientQuery, GetByIdReportsPatientResponse>
        {
            private readonly IMapper _mapper;
            private readonly IReportRepository _reportRepository;
            private readonly IAuthService _authService;
            private readonly  ReportBusinessRules _reportBusinessRules;

            public GetByIdReportsPatientQueryHandler(IMapper mapper, IReportRepository reportRepository, IAuthService authService, ReportBusinessRules reportBusinessRules)
            {
                _mapper = mapper;
                _reportRepository = reportRepository;
                _authService = authService;
                _reportBusinessRules = reportBusinessRules;
            }

            public async Task<GetByIdReportsPatientResponse> Handle(GetByIdReportsPatientQuery request, CancellationToken cancellationToken)
            {
                var patientId = await _authService.GetAuthenticatedUserIdAsync();

                await _reportBusinessRules.IsReportExistbyReportId(request.ReportId);
                await _reportBusinessRules.IsReportBelongsToUser(request.ReportId, patientId);
                

                var report = await _reportRepository.GetAsync(
               predicate: r => r.Id== request.ReportId && r.Appointment.PatientId == patientId,
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
               enableTracking: false,
               cancellationToken: cancellationToken
               );

                return _mapper.Map< GetByIdReportsPatientResponse >(report);
           
            }
        }
    }
}
