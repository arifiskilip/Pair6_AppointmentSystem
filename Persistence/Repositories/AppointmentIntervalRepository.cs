using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    internal class AppointmentIntervalRepository : EfRepositoryBase<AppointmentInterval, int, AppointmentSystemContext>, IAppointmentIntervalRepository
    {
        public AppointmentIntervalRepository(AppointmentSystemContext context) : base(context)
        {
        }
    }
}
