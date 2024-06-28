using Application.Repositories;
using Application.Services;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task IsReportExistbyReportId(int reportId)
        {
            var reportCheck = await _reportRepository.GetAsync(x => x.Id == reportId);

            if (reportCheck is null)
            {
                throw new BusinessException("Böyle bir rapor bulunamadı");
            }
        }

        public async Task IsReportBelongsToUser(int reportId, int userId)
        {
            var belonging = await _reportRepository.GetAsync(
                predicate:x=> x.Id == reportId && x.Appointment.PatientId == userId,
                include:query=> query
                .Include(i=> i.Appointment)
            );

            if(belonging is null)
            {
                throw new BusinessException("Kendi randevuluariniza ait olmayan raporlari goruntuleyemezsiniz");
            }
        }
    }
}
