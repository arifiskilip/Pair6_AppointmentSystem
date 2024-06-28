using Application.Repositories;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using Domain.Enums;
using MediatR;

namespace Application.Features.Appointment.Queries.GetPatientDashboardModel
{
    public class GetPatientDashboardModelQuery : IRequest<GetPatientDashboardModelResponse>, ISecuredRequest
    {
        public string[] Roles => ["Patient", "Admin"];


        public class GetPatientDashboardModelQueryHandler : IRequestHandler<GetPatientDashboardModelQuery, GetPatientDashboardModelResponse>
        {
            private readonly IAuthService _authService;
            private readonly IAppointmentRepository _appointmentRepository;

            public GetPatientDashboardModelQueryHandler(IAuthService authService, IAppointmentRepository appointmentRepository)
            {
                _authService = authService;
                _appointmentRepository = appointmentRepository;
            }

            public async Task<GetPatientDashboardModelResponse> Handle(GetPatientDashboardModelQuery request, CancellationToken cancellationToken)
            {
                var patientId = await _authService.GetAuthenticatedUserIdAsync();
                int patientTotalAppointments = _appointmentRepository.GetListNotPagedAsync(
                    predicate: x => x.IsDeleted == false && x.PatientId == patientId).Result.Count();
                int patientCompletedAppointments = _appointmentRepository.GetListNotPagedAsync(
                    predicate: x => x.IsDeleted == false && x.PatientId == patientId && x.AppointmentStatusId == (int)AppointmentStatusEnum.Completed).Result.Count();
                int patientCanceledAppointments = _appointmentRepository.GetListNotPagedAsync(
                    predicate: x => x.IsDeleted == false && x.PatientId == patientId && x.AppointmentStatusId == (int)AppointmentStatusEnum.Canceled).Result.Count();
                int patientWaitingAppointments = _appointmentRepository.GetListNotPagedAsync(
                    predicate: x => x.IsDeleted == false && x.PatientId == patientId && x.AppointmentStatusId == (int)AppointmentStatusEnum.Created).Result.Count();

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
