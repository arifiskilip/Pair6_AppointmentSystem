using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class DoctorScheduleRepository : EfRepositoryBase<DoctorSchedule, int, AppointmentSystemContext>, IDoctorScheduleRepository
    {
        public DoctorScheduleRepository(AppointmentSystemContext context) : base(context)
        {
        }
    }
}
