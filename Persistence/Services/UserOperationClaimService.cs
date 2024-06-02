using Application.Repositories;
using Application.Services;
using Domain.Entities;

namespace Persistence.Services
{
    public class UserOperationClaimService : IUserOperationClaimService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public UserOperationClaimService(IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task<UserOperationClaim> AddAsync(UserOperationClaim userOperationClaim)
        {
            var result = await _userOperationClaimRepository.AddAsync(userOperationClaim);
            return result;
        }
    }
}
