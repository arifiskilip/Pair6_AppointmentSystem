using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class AppointmentRepository : EfRepositoryBase<Appointment, int, AppointmentSystemContext>, IAppointmentRepository
    {
        public AppointmentRepository(AppointmentSystemContext context) : base(context)
        {
        }
    }
}
