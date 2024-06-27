using Application.Repositories;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcers.Exceptions.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Appointment.Commands.CancelByPatient
{
    public class CancelAppointmentByPatientCommand : IRequest<CancelAppointmentByPatientResponse>,ISecuredRequest
    {
        public int AppointmentId { get; set; }

        public string[] Roles => ["Patient"];
        public class CancelAppointmentByPatientCommandHandler : IRequestHandler<CancelAppointmentByPatientCommand, CancelAppointmentByPatientResponse>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IAppointmentIntervalService _appointmentIntervalService;

            public CancelAppointmentByPatientCommandHandler(IAppointmentRepository appointmentRepository, IHttpContextAccessor httpContextAccessor, IAppointmentIntervalService appointmentIntervalService)
            {
                _appointmentRepository = appointmentRepository;
                _httpContextAccessor = httpContextAccessor;
                _appointmentIntervalService = appointmentIntervalService;
            }
            public async Task<CancelAppointmentByPatientResponse> Handle(CancelAppointmentByPatientCommand request, CancellationToken cancellationToken)
            {
                //rules 
                //1. iptal ettigi kendine ait olmali
                //2. gecmise ait bir randevuyu iptal edemez

                var patientId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int parsedPatientId = int.Parse(patientId);

                var appointment = await _appointmentRepository.GetAsync(x => x.Id == request.AppointmentId, enableTracking: false);
                if (appointment == null)
                {
                    throw new BusinessException("Randevu Bulunamadı");
                }

                // Rule 1: Ensure the appointment belongs to the current patient
                if (appointment.PatientId != parsedPatientId)
                {
                    throw new BusinessException("Sadece kendinize ait olan randevuları iptal edebilirsiniz");
                }

                // Randevu aralığını ID ile getir
                var appointmentInterval = await _appointmentIntervalService.GetAsync(appointment.AppointmentIntervalId, enableTracking: false);

                // Kural 2: Randevu geçmişte olmamalı
                if (appointmentInterval.IntervalDate < DateTime.Now)
                {
                    throw new BusinessException("Geçmişteki bir randevuyu iptal edemezsiniz");
                }

                // Randevu durumunu iptal olarak güncelle (2 ID'si iptal için varsayılıyor)
                appointment.AppointmentStatusId = 2;

                // Randevu aralığı durumunu tekrar uygun olarak güncelle (1 ID'si uygun için varsayılıyor)
                appointmentInterval.AppointmentStatusId = 1;

                // Değişiklikleri depoya kaydet
                await _appointmentRepository.UpdateAsync(appointment);
                await _appointmentIntervalService.UpdateAsync(appointmentInterval);

                return new();
            }
        }
    }
}
