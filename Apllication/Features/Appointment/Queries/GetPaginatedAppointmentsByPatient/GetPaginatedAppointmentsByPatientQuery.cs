using Application.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.Appointment.Queries.GetPaginatedAppointmentsByPatient
{
    public class GetPaginatedAppointmentsByPatientQuery : IRequest<GetPaginatedAppointmentsByPatientResponse>
    {
        public int? PatientId { get; set; }
        public DateTime? Date { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public class GetPaginatedAppointmentsByPatientQueryHandler : IRequestHandler<GetPaginatedAppointmentsByPatientQuery, GetPaginatedAppointmentsByPatientResponse>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IMapper _mapper;

            public GetPaginatedAppointmentsByPatientQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
            {
                _appointmentRepository = appointmentRepository;
                _mapper = mapper;
            }

            public async Task<GetPaginatedAppointmentsByPatientResponse> Handle(GetPaginatedAppointmentsByPatientQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Domain.Entities.Appointment, bool>> predicate = a => true;

                if (request.PatientId.HasValue && request.PatientId > 0)
                {
                    predicate =  a => a.IsDeleted == false && a.PatientId == request.PatientId && a.AppointmentStatusId == (int)AppointmentStatusEnum.Completed || a.AppointmentStatusId == (int)AppointmentStatusEnum.Canceled;
                }

                if (request.Date.HasValue)
                {
                    if (request.PatientId.HasValue && request.PatientId > 0)
                    {
                        predicate = a => a.IsDeleted == false && a.PatientId == request.PatientId && a.AppointmentInterval.IntervalDate.Date == request.Date.Value.Date && a.AppointmentStatusId == (int)AppointmentStatusEnum.Completed || a.AppointmentStatusId == (int)AppointmentStatusEnum.Canceled;
                    }
                    else
                    {
                        predicate = a => a.IsDeleted == false && a.AppointmentInterval.IntervalDate.Date == request.Date.Value.Date && a.AppointmentStatusId == (int)AppointmentStatusEnum.Completed || a.AppointmentStatusId == (int)AppointmentStatusEnum.Canceled;
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
                    orderBy: x => x.OrderByDescending(x => x.AppointmentInterval.IntervalDate),
                    enableTracking: false,
                    cancellationToken: cancellationToken
                );

                var appointmentDtos = _mapper.Map<List<AppointmentPatientDto>>(appointments.Items);

                return new GetPaginatedAppointmentsByPatientResponse
                {
                    Appointments = new Paginate<AppointmentPatientDto>(appointmentDtos.AsQueryable(), appointments.Pagination)
                };
            }
        }
    }
}
