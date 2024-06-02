using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, int, AppointmentSystemContext>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(AppointmentSystemContext context) : base(context)
        {
        }
    }
}
