using Domain.Entities;

namespace Application.Services
{
    public interface IAppointmentIntervalService
    {
        Task<AppointmentInterval> AddAsync(AppointmentInterval appointmentInterval);
        Task<bool> IsAppointmentAvailableAsync(int  intervalId);
    }
}
