using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IUserRepository : IAsyncRepository<User, int> , IRepository<User, int>
    {
    }
}
