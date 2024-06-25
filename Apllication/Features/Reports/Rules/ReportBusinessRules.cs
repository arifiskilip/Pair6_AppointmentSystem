using Application.Repositories;
using Application.Services;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reports.Rules
{
    public class ReportBusinessRules : BaseBusinessRules
    {
        private readonly IReportRepository _reportRepository;
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentIntervalService _appointmentIntervalService;
        public ReportBusinessRules(IReportRepository reportRepository, IAppointmentService appointmentService, IAppointmentIntervalService appointmentIntervalService)
        {
            _reportRepository = reportRepository;
            _appointmentService = appointmentService;
            _appointmentIntervalService = appointmentIntervalService;
        }
        public async Task IsAppointmentExist(int appointmentId)
        {
            var appointment = await _appointmentService.GetAsync(appointmentId);
            if (appointment is null)
            {
                throw new BusinessException("Böyle bir randevu bulunmamakta");
            }
        }

        public async Task IsReportExist(int appointmentId)
        {
            var reportCheck = await _reportRepository.GetAsync(x => x.AppointmentId == appointmentId);

            if(reportCheck is not null)
            {
                throw new BusinessException("Bu randevuya ait rapor zaten mevcut");
            }
        }

        public async Task IsAppointmentBelongstoDoctor(int appointmentId, int doctorId)
        {
            var appointment = await _appointmentService.GetAsync(appointmentId);
            var appointmentInterval = await _appointmentIntervalService.GetAsync(appointment.AppointmentIntervalId);
            if (appointmentInterval.DoctorId != doctorId)
            {
                throw new BusinessException("Sadece kendi randevuluarınızın hastalarına rapor oluşturabilirsiniz.");
            }
        }
    }
}
