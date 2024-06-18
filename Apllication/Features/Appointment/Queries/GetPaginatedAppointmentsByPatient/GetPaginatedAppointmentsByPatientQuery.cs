using Application.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
                    predicate = a => a.PatientId == request.PatientId;
                }

                if (request.Date.HasValue)
                {
                    if (request.PatientId.HasValue && request.PatientId > 0)
                    {
                        predicate = a => a.PatientId == request.PatientId && a.AppointmentInterval.IntervalDate.Date == request.Date.Value.Date;
                    }
                    else
                    {
                        predicate = a => a.AppointmentInterval.IntervalDate.Date == request.Date.Value.Date;
                    }
                }

                var appointments = await _appointmentRepository.GetListAsync(
                    predicate: predicate,
                    include: query => query
                        .Include(a => a.AppointmentInterval)
                            .ThenInclude(ai => ai.AppointmentStatus)
                        .Include(a => a.AppointmentInterval)
                            .ThenInclude(ai => ai.Doctor),
                    index: request.PageIndex,
                    size: request.PageSize,
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
