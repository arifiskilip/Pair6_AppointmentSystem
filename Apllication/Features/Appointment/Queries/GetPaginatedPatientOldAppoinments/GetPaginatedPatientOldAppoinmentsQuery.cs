using Application.Features.Appointment.Queries.GetPaginatedPatientAppoinments;
using Application.Features.Appointment.Queries.GetPaginatedPatientNewAppoinments;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointment.Queries.GetPaginatedPatientOldAppoinments
{
    public class GetPaginatedPatientOldAppoinmentsQuery : IRequest<GetPaginatedPatientOldAppoinmentsResponse>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string[] Roles => ["Patient", "Admin", "Doctor"];



        public class GetPaginatedPatientOldAppoinmentsQueryHandler : IRequestHandler<GetPaginatedPatientOldAppoinmentsQuery, GetPaginatedPatientOldAppoinmentsResponse>
        {
            private readonly IMapper _mapper;
            private readonly IAuthService _authService;
            private readonly IPatientRepository _patientRepository;
            private readonly IAppointmentRepository _appointmentRepository;

            public GetPaginatedPatientOldAppoinmentsQueryHandler(IMapper mapper, IAuthService authService, IPatientRepository patientRepository, IAppointmentRepository appointmentRepository)
            {
                _mapper = mapper;
                _authService = authService;
                _patientRepository = patientRepository;
                _appointmentRepository = appointmentRepository;
            }

            public async Task<GetPaginatedPatientOldAppoinmentsResponse> Handle(GetPaginatedPatientOldAppoinmentsQuery request, CancellationToken cancellationToken)
            {
                var patientId = await _authService.GetAuthenticatedUserIdAsync();

                var patient = await _patientRepository.GetAsync(x => x.Id == patientId);

                var appointments = await _appointmentRepository.GetListAsync(
                    predicate: x => x.PatientId == patientId && x.AppointmentInterval.IntervalDate < DateTime.Now,
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

                return new GetPaginatedPatientOldAppoinmentsResponse
                {
                    PatientAppointments = new Paginate<PatientAppointmentDto>(appointmentDtos.AsQueryable(), appointments.Pagination)
                };
            }
        }
    }
}
