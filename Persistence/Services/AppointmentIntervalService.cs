using Application.Repositories;
using Application.Services;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.Query;

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

        public async Task<AppointmentInterval> GetAsync(int appointmentIntervalId,
            Func<IQueryable<AppointmentInterval>, IIncludableQueryable<AppointmentInterval, object>>? include = null,
            bool enableTracking = true)
        {
            AppointmentInterval? appointmentInterval = await _appointmentIntervalRepository.GetAsync(x=> x.Id == appointmentIntervalId,include,enableTracking);
            if (appointmentInterval is not null)
            {
                return appointmentInterval;
            }
            throw new BusinessException("Randevu aralığı mevcut değil.");
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

        public async Task<AppointmentInterval> UpdateAsync(AppointmentInterval appointmentInterval)
        {
            await _appointmentIntervalRepository.UpdateAsync(appointmentInterval);
            return appointmentInterval;
        }
    }
}
