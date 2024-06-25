using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Features.Appointment.Queries.GetPaginatedPatientAppoinments
{
    public class GetPaginatedPatientAppointmentsQuery : IRequest<GetPaginatedPatientAppointmentsResponse>,ISecuredRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string[] Roles => ["Patient"];

        public class GetPaginatedPatientAppointmentsQueryHandler : IRequestHandler<GetPaginatedPatientAppointmentsQuery, GetPaginatedPatientAppointmentsResponse>
        {
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IPatientRepository _patientRepository;
            private readonly IAppointmentRepository _appointmentRepository;

            public GetPaginatedPatientAppointmentsQueryHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, IPatientRepository patientRepository, IAppointmentRepository appointmentRepository)
            {
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
                _patientRepository = patientRepository;
                _appointmentRepository = appointmentRepository;
            }

            public async Task<GetPaginatedPatientAppointmentsResponse> Handle(GetPaginatedPatientAppointmentsQuery request, CancellationToken cancellationToken)
            {
              
                var patientId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var patient = await _patientRepository.GetAsync(x => x.Id == int.Parse(patientId));

                var appointments = await _appointmentRepository.GetListAsync(
                    predicate: x => x.PatientId == int.Parse(patientId),
                    include: query => query
                        .Include(a => a.Patient)
                        .Include(a => a.AppointmentStatus)
                        .Include(a => a.AppointmentInterval)
                            .ThenInclude(a => a.Doctor)
                                .ThenInclude(a => a.Branch)
                        .Include(a => a.AppointmentInterval)
                            .ThenInclude(ai => ai.Doctor)
                                .ThenInclude(d => d.Title),
                    orderBy: q => q.OrderByDescending(a => a.AppointmentInterval.IntervalDate),
                    index: request.PageIndex,
                    size: request.PageSize,
                    enableTracking: false,
                    cancellationToken: cancellationToken
                    );
                var appointmentDtos = _mapper.Map<List<PatientAppointmentDto>>(appointments.Items);

                return new GetPaginatedPatientAppointmentsResponse
                {
                    PatientAppointments = new Paginate<PatientAppointmentDto>(appointmentDtos.AsQueryable(), appointments.Pagination)
                };
              
            }
        }
    }
}
