using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IPatientRepository : IAsyncRepository<Patient, int>, IRepository<Patient, int>
    {

    }
}
