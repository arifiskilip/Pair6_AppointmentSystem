using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointment.Queries.GetAppointmentsForCurrentDayByDoctor
{
    public class GetAppointmentsForCurrentDayByDoctorQuery : IRequest<List<GetAppointmentsForCurrentDayByDoctorResponse>>, ISecuredRequest
    {
        public string[] Roles => ["Doctor"];


        public class GetAppointmentsForCurrentDayByDoctorQueryHander : IRequestHandler<GetAppointmentsForCurrentDayByDoctorQuery, List<GetAppointmentsForCurrentDayByDoctorResponse>>
        {
            private readonly IAuthService _authService;
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IMapper _mapper;

            public GetAppointmentsForCurrentDayByDoctorQueryHander(IAuthService authService, IAppointmentRepository appointmentRepository, IMapper mapper)
            {
                _authService = authService;
                _appointmentRepository = appointmentRepository;
                _mapper = mapper;
            }

            public async Task<List<GetAppointmentsForCurrentDayByDoctorResponse>> Handle(GetAppointmentsForCurrentDayByDoctorQuery request, CancellationToken cancellationToken)
            {
                var doctorId = await _authService.GetAuthenticatedUserIdAsync();
                var appointments = await _appointmentRepository.GetListNotPagedAsync(
                    predicate: x => x.IsDeleted == false && x.AppointmentInterval.DoctorId == doctorId &&
                            x.AppointmentInterval.IntervalDate.Date == DateTime.Now.Date,
                    orderBy: x => x.OrderBy(o => o.AppointmentInterval.IntervalDate),
                    include: x => x.Include(i => i.Patient).Include(i => i.AppointmentInterval).Include(i => i.AppointmentStatus),
                    enableTracking: false);

                return _mapper.Map<List<GetAppointmentsForCurrentDayByDoctorResponse>>(appointments);
            }
        }
    }
}
