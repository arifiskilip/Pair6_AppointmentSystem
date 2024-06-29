using Application.Repositories;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointment.Queries.GetDoctorDashboardModel
{
    public class GetDoctorDashboardModelQuery : IRequest<GetDoctorDashboardModelResponse> , ISecuredRequest
    {
        public string[] Roles => ["Doctor", "Admin"];


        public class GetDoctorDashboardModelQueryHandler : IRequestHandler<GetDoctorDashboardModelQuery, GetDoctorDashboardModelResponse>
        {
            private readonly IAuthService _authService;
            private readonly IAppointmentRepository _appointmentRepository;

            public GetDoctorDashboardModelQueryHandler(IAuthService authService, IAppointmentRepository appointmentRepository)
            {
                _authService = authService;
                _appointmentRepository = appointmentRepository;
            }

            public async Task<GetDoctorDashboardModelResponse> Handle(GetDoctorDashboardModelQuery request, CancellationToken cancellationToken)
            {
                var doctorId = await _authService.GetAuthenticatedUserIdAsync();
                int patientTotalAppointments = _appointmentRepository.GetListNotPagedAsync(
                    predicate: x => x.IsDeleted == false && x.AppointmentInterval.DoctorId == doctorId,
                    include:x=> x.Include(i=> i.AppointmentInterval)).Result.Count();
                int patientCompletedAppointments = _appointmentRepository.GetListNotPagedAsync(
                    predicate: x => x.IsDeleted == false && x.AppointmentInterval.DoctorId == doctorId && x.AppointmentStatusId == (int)AppointmentStatusEnum.Completed,
                    include: x => x.Include(i => i.AppointmentInterval)).Result.Count();
                int patientCanceledAppointments = _appointmentRepository.GetListNotPagedAsync(
                    predicate: x => x.IsDeleted == false && x.AppointmentInterval.DoctorId == doctorId && x.AppointmentStatusId == (int)AppointmentStatusEnum.Canceled,
                    include: x => x.Include(i => i.AppointmentInterval)).Result.Count();
                int patientWaitingAppointments = _appointmentRepository.GetListNotPagedAsync(
                    predicate: x => x.IsDeleted == false && x.AppointmentInterval.DoctorId == doctorId && x.AppointmentStatusId == (int)AppointmentStatusEnum.Created,
                    include: x => x.Include(i => i.AppointmentInterval)).Result.Count();

                return new()
                {
                    CanceledAppointment = patientCanceledAppointments,
                    CompletedAppointment = patientCompletedAppointments,
                    TotalAppointment = patientTotalAppointments,
                    WaitinAppointment = patientWaitingAppointments
                };
            }
        }
    }
}
