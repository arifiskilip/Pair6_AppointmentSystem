using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAppointmentService
    {
        Task<Appointment> GetAsync(int appointmentIntervalId, Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>? include = null,
            bool enableTracking = true);
    }
}
