using Application.Repositories;
using Application.Services;
using Domain.Entities;

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
    }
}
