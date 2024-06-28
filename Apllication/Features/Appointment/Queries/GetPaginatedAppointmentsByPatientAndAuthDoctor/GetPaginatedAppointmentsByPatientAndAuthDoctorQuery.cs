using Application.Features.Appointment.Queries.GetPaginatedAppointmentsByPatient;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.Appointment.Queries.GetPaginatedPatientByDoctorId
{
    public class GetPaginatedAppointmentsByPatientAndAuthDoctorQuery : IRequest<GetPaginatedAppointmentsByPatientAndAuthDoctorResponse>, ISecuredRequest
    {
        public int? PatientId { get; set; }
        public DateTime? Date { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string[] Roles => ["Doctor"];

        public class GetPaginatedAppointmentsByPatientAndAuthDoctorQueryHandler : IRequestHandler<GetPaginatedAppointmentsByPatientAndAuthDoctorQuery, GetPaginatedAppointmentsByPatientAndAuthDoctorResponse>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IMapper _mapper;
            private readonly IAuthService _authService;

            public GetPaginatedAppointmentsByPatientAndAuthDoctorQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper, IAuthService authService)
            {
                _appointmentRepository = appointmentRepository;
                _mapper = mapper;
                _authService = authService;
            }

            public async Task<GetPaginatedAppointmentsByPatientAndAuthDoctorResponse> Handle(GetPaginatedAppointmentsByPatientAndAuthDoctorQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Domain.Entities.Appointment, bool>> predicate = a => true;
                var doctorId = await _authService.GetAuthenticatedUserIdAsync();

                if (request.PatientId.HasValue && request.PatientId > 0)
                {
                    predicate = a => a.IsDeleted == false && a.PatientId == request.PatientId && a.AppointmentStatusId == (int)AppointmentStatusEnum.Completed || a.AppointmentStatusId == (int)AppointmentStatusEnum.Canceled && a.AppointmentInterval.DoctorId == doctorId;
                }

                if (request.Date.HasValue)
                {
                    if (request.PatientId.HasValue && request.PatientId > 0)
                    {
                        predicate = a => a.IsDeleted == false && a.PatientId == request.PatientId && a.AppointmentInterval.IntervalDate.Date == request.Date.Value.Date && a.AppointmentStatusId == (int)AppointmentStatusEnum.Completed || a.AppointmentStatusId == (int)AppointmentStatusEnum.Canceled && a.AppointmentInterval.DoctorId == doctorId;
                    }
                    else
                    {
                        predicate = a => a.IsDeleted == false && a.AppointmentInterval.IntervalDate.Date == request.Date.Value.Date && a.AppointmentStatusId == (int)AppointmentStatusEnum.Completed || a.AppointmentStatusId == (int)AppointmentStatusEnum.Canceled && a.AppointmentInterval.DoctorId == doctorId;
                    }
                }

                var appointments = await _appointmentRepository.GetListAsync(
                    predicate: predicate,
                    include: query => query
                        .Include(a => a.AppointmentInterval)
                        .ThenInclude(ai => ai.Doctor)
                            .ThenInclude(d => d.Branch)
                        .Include(a => a.AppointmentInterval)
                            .ThenInclude(ai => ai.Doctor)
                                .ThenInclude(d => d.Title)
                        .Include(a => a.AppointmentStatus),
                    index: request.PageIndex,
                    size: request.PageSize,
                    orderBy: x=> x.OrderByDescending(x=>x.AppointmentInterval.IntervalDate),
                    enableTracking: false,
                    cancellationToken: cancellationToken
                );

                var appointmentDtos = _mapper.Map<List<AppointmentPatientDto>>(appointments.Items);

                return new GetPaginatedAppointmentsByPatientAndAuthDoctorResponse
                {
                    Appointments = new Paginate<AppointmentPatientDto>(appointmentDtos.AsQueryable(), appointments.Pagination)
                };
            }
        }
    }
}
