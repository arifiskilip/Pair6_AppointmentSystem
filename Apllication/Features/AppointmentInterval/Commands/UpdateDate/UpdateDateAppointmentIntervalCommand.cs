using Application.Features.Appointment.Rules;
using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Enums;
using MediatR;

namespace Application.Features.AppointmentInterval.Commands.UpdateDate
{
    public class UpdateDateAppointmentIntervalCommand : IRequest<UpdateDateAppointmentIntervalResponse>, ISecuredRequest
    {
        public int AppointmentIntervalId { get; set; }
        public DateTime IntervalDate { get; set; }

        public string[] Roles => ["Doctor"];


        public class UpdateDateAppointmentIntervalCommandHandler : IRequestHandler<UpdateDateAppointmentIntervalCommand, UpdateDateAppointmentIntervalResponse>
        {
            private readonly IAppointmentIntervalRepository _appointmentIntervalRepository;
            private readonly AppointmentBusinessRules _businessRules;
            private readonly IMapper _mapper;

            public UpdateDateAppointmentIntervalCommandHandler(IAppointmentIntervalRepository appointmentIntervalRepository, AppointmentBusinessRules businessRules, IMapper mapper)
            {
                _appointmentIntervalRepository = appointmentIntervalRepository;
                _businessRules = businessRules;
                _mapper = mapper;
            }

            public async Task<UpdateDateAppointmentIntervalResponse> Handle(UpdateDateAppointmentIntervalCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.AppointmentIntervalAvailable(request.AppointmentIntervalId);
                var checkAppointmentInterval = await _appointmentIntervalRepository.GetAsync(
                    predicate: x => x.Id == request.AppointmentIntervalId,
                    enableTracking:true);
                if (checkAppointmentInterval.AppointmentStatusId != (int)AppointmentStatusEnum.Available)
                {
                    throw new BusinessException("Hastanın işlem yaptığı randevu hakkında güncelleme operasyonu yapılamaz.");
                }
                if (request.IntervalDate.Date < DateTime.Now.Date)
                {
                    throw new BusinessException("Mevcut tarihten önceki bir güne randevu seçilemez.");
                }

                var existingInterval = await _appointmentIntervalRepository.GetAsync(
               predicate: x => x.IntervalDate == request.IntervalDate && x.Id != request.AppointmentIntervalId && x.IsDeleted == false);

                if (existingInterval != null)
                {
                    throw new BusinessException("Aynı tarihe sahip bir randevu aralığı zaten mevcut.");
                }
                // Update the interval date
                checkAppointmentInterval.IntervalDate = request.IntervalDate;
                await _appointmentIntervalRepository.UpdateAsync(checkAppointmentInterval);

                // Create response
                var response = _mapper.Map<UpdateDateAppointmentIntervalResponse>(checkAppointmentInterval);
                return response;
            }
        }
    }
}
