using Application.Repositories;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transaction;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Enums;
using MediatR;

namespace Application.Features.Appointment.Commands.CompleteByDoctor
{
    public class CompleteAppointmentByDoctorCommand : IRequest<CompleteAppointmentByDoctorResponse> ,ISecuredRequest, ITransactionalRequest
    {
        public int AppointmentId { get; set; }

        public string[] Roles => ["Doctor"];



        public class CompleteAppointmentByDoctorCommandHandler : IRequestHandler<CompleteAppointmentByDoctorCommand, CompleteAppointmentByDoctorResponse>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IAuthService _authService;
            private readonly IAppointmentIntervalService _appointmentIntervalService;

            public CompleteAppointmentByDoctorCommandHandler(IAppointmentRepository appointmentRepository, IAuthService authService, IAppointmentIntervalService appointmentIntervalService)
            {
                _appointmentRepository = appointmentRepository;
                _authService = authService;
                _appointmentIntervalService = appointmentIntervalService;
            }

            public async Task<CompleteAppointmentByDoctorResponse> Handle(CompleteAppointmentByDoctorCommand request, CancellationToken cancellationToken)
            {
                var doctorId = await _authService.GetAuthenticatedUserIdAsync();

                var appointment = await _appointmentRepository.GetAsync(x => x.Id == request.AppointmentId);
                if (appointment == null)
                {
                    throw new BusinessException("Randevu bulunamadı");
                }
                var appointmentInterval = await _appointmentIntervalService.GetAsync(appointment.AppointmentIntervalId);
                if (appointmentInterval.DoctorId != doctorId)
                {
                    throw new BusinessException("You can only cancel your own appointments");
                }
                if (appointmentInterval.IntervalDate < DateTime.Now)
                {
                    throw new BusinessException("Geçmişteki bir randevuyu onaylayamazsın.");
                }

                appointment.AppointmentStatusId = (int)AppointmentStatusEnum.Completed;

                appointmentInterval.AppointmentStatusId = (int)AppointmentStatusEnum.Completed;

                // Değişiklikleri depoya kaydet
                await _appointmentRepository.UpdateAsync(appointment);
                await _appointmentIntervalService.UpdateAsync(appointmentInterval);

                return new();
            }
        }
    }
}
