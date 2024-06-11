using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IUserRepository : IAsyncRepository<User, int> , IRepository<User, int>
    {
        Task<IList<Core.Security.Entitites.OperationClaim>> GetClaimsAsync(User user);
    }
}
