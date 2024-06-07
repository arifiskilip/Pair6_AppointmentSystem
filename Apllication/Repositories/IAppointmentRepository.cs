using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IAppointmentRepository : IAsyncRepository<Appointment,int>, IRepository<Appointment,int>
    {
    }
}
