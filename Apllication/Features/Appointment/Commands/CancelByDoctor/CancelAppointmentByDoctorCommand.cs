using Application.Repositories;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Appointment.Commands.CancelByDoctor
{
    public class CancelAppointmentByDoctorCommand : IRequest<CancelAppointmentByDoctorResponse>, ISecuredRequest
    {
        public int AppointmentId { get; set; }

        public string[] Roles => ["Doctor"];


        public class CancelAppointmentByDoctorCommandHandler : IRequestHandler<CancelAppointmentByDoctorCommand, CancelAppointmentByDoctorResponse>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IAppointmentIntervalService _appointmentIntervalService;

            public CancelAppointmentByDoctorCommandHandler(IAppointmentRepository appointmentRepository, IHttpContextAccessor httpContextAccessor, IAppointmentIntervalService appointmentIntervalService)
            {
                _appointmentRepository = appointmentRepository;
                _httpContextAccessor = httpContextAccessor;
                _appointmentIntervalService = appointmentIntervalService;
            }

            public async Task<CancelAppointmentByDoctorResponse> Handle(CancelAppointmentByDoctorCommand request, CancellationToken cancellationToken)
            {
                var doctorId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int parsedDoctorId = int.Parse(doctorId);

                var appointment = await _appointmentRepository.GetAsync(x => x.Id == request.AppointmentId);
                if (appointment == null)
                {
                    throw new BusinessException("Randevu bulunamadı");
                }
                var appointmentInterval = await _appointmentIntervalService.GetAsync(appointment.AppointmentIntervalId);
                if (appointmentInterval.DoctorId != parsedDoctorId)
                {
                    throw new BusinessException("You can only cancel your own appointments");
                }
                if (appointmentInterval.IntervalDate < DateTime.Now)
                {
                    throw new BusinessException("Geçmişteki bir randevuyu iptal edemezsiniz");
                }

                // Randevu durumunu iptal olarak güncelle (2 ID'si iptal için varsayılıyor)
                appointment.AppointmentStatusId = (int)AppointmentStatusEnum.Canceled;

                // Randevu aralığı durumunu tekrar uygun olarak güncelle (1 ID'si uygun için varsayılıyor)
                appointmentInterval.AppointmentStatusId = (int)AppointmentStatusEnum.Available;

                // Değişiklikleri depoya kaydet
                await _appointmentRepository.UpdateAsync(appointment);
                await _appointmentIntervalService.UpdateAsync(appointmentInterval);

                return new();
            }
        }
    }
}
