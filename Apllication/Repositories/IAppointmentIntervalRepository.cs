using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IAppointmentIntervalRepository : IAsyncRepository<AppointmentInterval,int>, IRepository<AppointmentInterval,int>
    {
    }
}
