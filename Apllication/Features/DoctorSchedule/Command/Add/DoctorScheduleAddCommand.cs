using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Enums;
using MediatR;

namespace Application.Features.DoctorSchedule.Command.Add
{
    public class DoctorScheduleAddCommand : IRequest<DoctorScheduleAddResponse>, ITransactionalRequest
    {
        public int DoctorId { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int PatientInterval { get; set; }


        public class DoctorScheduleAddHandler : IRequestHandler<DoctorScheduleAddCommand, DoctorScheduleAddResponse>
        {
            private readonly IDoctorScheduleRepository _doctorScheduleRepository;
            private readonly IMapper _mapper;
            private readonly IAppointmentIntervalService _appointmentIntervalService;

            public DoctorScheduleAddHandler(IDoctorScheduleRepository doctorScheduleRepository, IMapper mapper, IAppointmentIntervalService appointmentIntervalService)
            {
                _doctorScheduleRepository = doctorScheduleRepository;
                _mapper = mapper;
                _appointmentIntervalService = appointmentIntervalService;
            }

            public async Task<DoctorScheduleAddResponse> Handle(DoctorScheduleAddCommand request, CancellationToken cancellationToken)
            {
                // Rules

                //Date Control
                if (request.StartTime>= request.EndTime)
                {
                    throw new BusinessException("Başlangıç saati bitiş saatinden büyük olamaz.");
                }
                if (request.Day.Add(request.StartTime) < DateTime.Now )
                {
                    throw new BusinessException("Geçmiş tarihe yönelik program oluşturulamaz.");
                }

                // Check Doctor Schedule programs

                DateTime startDate = request.Day.Add(request.StartTime);
                DateTime endDate = request.Day.Add(request.EndTime);

                var result = await _doctorScheduleRepository.GetListNotPagedAsync(
                    predicate: x => x.DoctorId == request.DoctorId && x.Day == request.Day);
                if (result is not null)
                {
                    foreach (var interval in result)
                    {
                        if ((request.StartTime >= interval.StartTime && request.StartTime < interval.EndTime) ||
                    (request.EndTime > interval.StartTime && request.EndTime <= interval.EndTime) ||
                    (request.StartTime <= interval.StartTime && request.EndTime >= interval.EndTime))
                        {
                            throw new BusinessException("Girilen aralıklar değerinde zaten bir program oluşturulmuş.");
                        }
                    }
                }

                Domain.Entities.DoctorSchedule schedule = _mapper.Map<Domain.Entities.DoctorSchedule>(request);
                var addedSchedule = await _doctorScheduleRepository.AddAsync(schedule);
                
                var doctorSchedules = GenerateDoctorSchedules(request.Day.ToString("dd.MM.yyyy"),request.StartTime.ToString(@"hh\:mm"), request.EndTime.ToString(@"hh\:mm"), request.PatientInterval);

                foreach (var item in doctorSchedules)
                {
                    await _appointmentIntervalService.AddAsync(new()
                    {
                        AppointmentStatusId = (int)AppointmentStatusEnum.Available,
                        DoctorId = 1,
                        IntervalDate = item,
                    });
                }

                return _mapper.Map<DoctorScheduleAddResponse>(addedSchedule);

            }
            private List<DateTime> GenerateDoctorSchedules(string dateStr, string startTimeStr, string endTimeStr, int durationMinutes)
            {
                List<DateTime> appointmentTimes = new List<DateTime>();

                DateTime date = DateTime.ParseExact(dateStr, "dd.MM.yyyy", null);
                DateTime startTime = DateTime.ParseExact(startTimeStr, "HH:mm", null);
                DateTime endTime = DateTime.ParseExact(endTimeStr, "HH:mm", null);

                DateTime startDateTime = new DateTime(date.Year, date.Month, date.Day, startTime.Hour, startTime.Minute, 0);
                DateTime endDateTime = new DateTime(date.Year, date.Month, date.Day, endTime.Hour, endTime.Minute, 0);
                DateTime currentTime = startDateTime;

                while (currentTime < endDateTime)
                {
                    //appointmentTimes.Add(currentTime.ToString("dd.MM.yyyy HH:mm"));
                    appointmentTimes.Add(currentTime);
                    currentTime = currentTime.AddMinutes(durationMinutes);
                }

                return appointmentTimes;
            }
        }
        
    }
}





