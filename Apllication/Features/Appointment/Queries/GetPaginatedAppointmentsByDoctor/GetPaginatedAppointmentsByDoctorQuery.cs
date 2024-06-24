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

namespace Application.Features.Appointment.Queries.GetPaginatedAppointmentsByDoctor
{
    public class GetPaginatedAppointmentsByDoctorQuery : IRequest<GetPaginatedAppointmentsByDoctorResponse>
    {
        public int? DoctorId { get; set; }
        public DateTime? Date { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public class GetPaginatedAppointmentsByDoctorQueryHandler : IRequestHandler<GetPaginatedAppointmentsByDoctorQuery, GetPaginatedAppointmentsByDoctorResponse>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IMapper _mapper;

            public GetPaginatedAppointmentsByDoctorQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
            {
                _appointmentRepository = appointmentRepository;
                _mapper = mapper;
            }

            public async Task<GetPaginatedAppointmentsByDoctorResponse> Handle(GetPaginatedAppointmentsByDoctorQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Domain.Entities.Appointment, bool>> predicate = a => true;

                if (request.DoctorId > 0)
                {
                    predicate = a => a.AppointmentInterval.DoctorId == request.DoctorId && a.AppointmentStatusId == 4; ;
                }

                if (request.Date.HasValue)
                {
                    if (request.DoctorId > 0)
                    {
                        predicate = a => a.AppointmentInterval.DoctorId == request.DoctorId && a.AppointmentInterval.IntervalDate.Date == request.Date.Value.Date && a.AppointmentStatusId == 4;
                    }
                    else
                    {
                        predicate = a => a.AppointmentInterval.IntervalDate.Date == request.Date.Value.Date && a.AppointmentStatusId == 4;
                    }
                }


                var appointments = await _appointmentRepository.GetListAsync(
                    predicate: predicate,
                    include: query => query
                        .Include(a => a.Patient)
                        .Include(a => a.AppointmentInterval)
                        .Include(a => a.AppointmentStatus),
                    index: request.PageIndex,
                    size: request.PageSize,
                    enableTracking: false,
                    cancellationToken: cancellationToken
                );

                var appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments.Items);

                return new GetPaginatedAppointmentsByDoctorResponse
                {
                    Appointments = new Paginate<AppointmentDto>(appointmentDtos.AsQueryable(), appointments.Pagination)
                };
            }
        }
    }
}
