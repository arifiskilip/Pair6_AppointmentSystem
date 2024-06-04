using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IBranchRepository : IAsyncRepository<Branch, short>, IRepository<Branch, short>
    {
    }
}
