using Application.Repositories;
using Application.Services;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;
using Domain.Enums;

namespace Persistence.Services
{
    public class AppointmentIntervalService : IAppointmentIntervalService
    {
        private readonly IAppointmentIntervalRepository _appointmentIntervalRepository;

        public AppointmentIntervalService(IAppointmentIntervalRepository appointmentIntervalRepository)
        {
            _appointmentIntervalRepository = appointmentIntervalRepository;
        }

        public async Task<AppointmentInterval> AddAsync(AppointmentInterval appointmentInterval)
        {
            await _appointmentIntervalRepository.AddAsync(appointmentInterval);
            return appointmentInterval;
        }

        public async Task<bool> IsAppointmentAvailableAsync(int intervalId)
        {
            var checkInterval = await _appointmentIntervalRepository.GetAsync(x=> x.Id == intervalId);
            if (checkInterval is not null)
            {
                return (checkInterval.AppointmentStatusId == (int)AppointmentStatusEnum.Available || checkInterval.AppointmentStatusId == (int)AppointmentStatusEnum.Canceled ? true : false);
            }
            throw new BusinessException("Seçilen randevu mevcut değil!");
        }
    }
}
