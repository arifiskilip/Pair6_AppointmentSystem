using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Features.Appointment.Queries.GetPaginatedDoctorAppointments
{
    public class GetPaginatedDoctorAppointmentsQuery : IRequest<GetPaginatedDoctorAppointmentsResponse>, ISecuredRequest
    {
      
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string[] Roles => ["Doctor"];
        public class GetPaginatedDoctorAppointmentsQueryHandler : IRequestHandler<GetPaginatedDoctorAppointmentsQuery, GetPaginatedDoctorAppointmentsResponse>
        {
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IDoctorRepository _doctorRepository;
            private readonly IAppointmentRepository _appointmentRepository;

            public GetPaginatedDoctorAppointmentsQueryHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository)
            {
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
                _doctorRepository = doctorRepository;
                _appointmentRepository = appointmentRepository;
            }

            public async Task<GetPaginatedDoctorAppointmentsResponse> Handle(GetPaginatedDoctorAppointmentsQuery request, CancellationToken cancellationToken)
            {
               
                var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var doctor = await _doctorRepository.GetAsync(x => x.Id == int.Parse(doctorId));
 

                var appointments = await _appointmentRepository.GetListAsync(
                    predicate: x=> x.AppointmentInterval.DoctorId == int.Parse(doctorId),
                    include: query => query
                        .Include(a => a.Patient)
                        .Include(a => a.AppointmentStatus)
                        .Include(a => a.AppointmentInterval),
                    orderBy: q => q.OrderByDescending(a => a.AppointmentInterval.IntervalDate),
                    index: request.PageIndex,
                    size: request.PageSize,
                    enableTracking: false,
                    cancellationToken: cancellationToken
                );

                var appointmentDtos = _mapper.Map<List<DoctorAppointmentDto>>(appointments.Items);

                return new GetPaginatedDoctorAppointmentsResponse
                {
                    DoctorAppointments = new Paginate<DoctorAppointmentDto>(appointmentDtos.AsQueryable(), appointments.Pagination)
                };
            }
        }
    }
}
