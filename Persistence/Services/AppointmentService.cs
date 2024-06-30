using Application.Repositories;
using Application.Services;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class AppointmentService : IAppointmentService
    {
       private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Appointment> GetAsync(int appointmentId, Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>>? include = null, bool enableTracking = true)
        {
            Appointment appointment = await _appointmentRepository.GetAsync(x => x.Id == appointmentId);

            if(appointment is null)
            {
                throw new BusinessException("Randevu mevcut değil.");
            }
            return appointment;
        }

        public async Task<Appointment> UpdateAsunc(Appointment appointment)
        {
            await _appointmentRepository.UpdateAsync(appointment);
            return appointment;
        }
    }
}
