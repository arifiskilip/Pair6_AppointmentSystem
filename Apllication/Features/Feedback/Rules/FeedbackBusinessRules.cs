using Application.Features.Titles.Constants;
using Application.Repositories;
using Application.Services;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Rules
{
    public class FeedbackBusinessRules : BaseBusinessRules
    {
        private readonly IFeedbackRepository _feedbackRepository;

        private readonly IAppointmentService _appointmentService;

        public FeedbackBusinessRules(IFeedbackRepository feedbackRepository, IAppointmentService appointmentService)
        {
            _feedbackRepository = feedbackRepository;
            _appointmentService = appointmentService;
        }

        public async Task IsAppointmentStatusCompleted(int appointmentId)
        {
           var appointment = await  _appointmentService.GetAsync(appointmentId);
            if (appointment.AppointmentStatusId != (int)AppointmentStatusEnum.Completed)
            {
                throw new BusinessException("Sadece tamamlanmış randevularınız hakkında geri bildirimde bulunabilirsiniz.");
            }
        }

        public async Task IsAppointmentBelongsToPatient(int appointmentId,int patientId)
        {
            var appointment = await _appointmentService.GetAsync(appointmentId);
            if (appointment.PatientId != patientId)
            {
                throw new BusinessException("Sadece kendi randevuluarınıza geri bildiirmde bulunabilirsiniz.");
            }
        }

        public async Task IsAppointmentExist(int appointmentId)
        {
            var appointment = await _appointmentService.GetAsync(appointmentId);
            if (appointment is null)
            {
                throw new BusinessException("Böyle bir randevu bulunmamakta");
            }
        }

        public async Task IsFeedbackExist(int appointmentId)
        {
            var feedBack = await _feedbackRepository.GetAsync(predicate:x=> x.AppointmentId == appointmentId);
            if (feedBack is not null)
            {
                throw new BusinessException("Randevunuza ait geribildiriminiz zaten mevcut");
            }
        }

        public async Task IsFeedbackExistbyId(int feedBackId)
        {
            var feedBack = await _feedbackRepository.GetAsync(predicate: x => x.Id == feedBackId);
            if (feedBack is null)
            {
                throw new BusinessException("Böyle bir geri bildirim bulunamadı.");
            }
        }
    }
}
