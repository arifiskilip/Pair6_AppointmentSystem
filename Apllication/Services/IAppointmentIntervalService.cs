using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services
{
    public interface IAppointmentIntervalService
    {
        Task<AppointmentInterval> AddAsync(AppointmentInterval appointmentInterval);
        Task<AppointmentInterval> UpdateAsync(AppointmentInterval appointmentInterval);
        Task<AppointmentInterval> GetAsync(int appointmentIntervalId, Func<IQueryable<AppointmentInterval>, IIncludableQueryable<AppointmentInterval, object>>? include = null,
            bool enableTracking = true);
        Task<bool> IsAppointmentAvailableAsync(int  intervalId);
    }
}
