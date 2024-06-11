using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IDoctorRepository : IAsyncRepository<Doctor, int>, IRepository<Doctor, int>
    {
    }
}
