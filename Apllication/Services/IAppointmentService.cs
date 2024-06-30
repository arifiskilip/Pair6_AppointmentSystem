using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services
{
    public interface IAppointmentService
    {
        Task<Appointment> GetAsync(int appointmentIntervalId, Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>? include = null,
            bool enableTracking = true);
        Task<Appointment> UpdateAsunc(Appointment appointment);
    }
}
