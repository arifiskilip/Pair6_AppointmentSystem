using Application.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Features.Appointment.Queries.GetPaginatedAppointmentsByDoctor
{
    public class GetPaginatedAppointmentsByDoctorQuery : IRequest<GetPaginatedAppointmentsByDoctorResponse>
    {
        public int? DoctorId { get; set; }
        public int? AppointmentStatusId { get; set; }
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
                var result = await _appointmentRepository.GetPaginatedFilteredByDoctorAsync(request.DoctorId, request.AppointmentStatusId, request.Date, request.PageIndex, request.PageSize);

                var appointmentDtos = _mapper.Map<List<AppointmentDto>>(result.Items);

                return new GetPaginatedAppointmentsByDoctorResponse
                {
                    Appointments = new Paginate<AppointmentDto>(appointmentDtos.AsQueryable(), result.Pagination)
                };
            }
        }
    }
}
