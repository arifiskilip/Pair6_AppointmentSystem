using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IUserOperationClaimRepository : IAsyncRepository<UserOperationClaim, int>, IRepository<UserOperationClaim, int>
    {
    }
}
