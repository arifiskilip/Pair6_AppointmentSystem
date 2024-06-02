using Domain.Entities;

namespace Application.Services
{
    public interface IUserOperationClaimService
    {
        Task<UserOperationClaim> AddAsync(UserOperationClaim userOperationClaim);
    }
}
