using Application.Repositories;
using Application.Services;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task SetCustomerEmailVerified(int userId)
        {
            var checkUser = await _userRepository.GetAsync(x => x.Id == userId);
            if (checkUser is not null)
            {
                checkUser.IsEmailVerified = true;
                await _userRepository.UpdateAsync(checkUser);
            }
            throw new BusinessException("User not found!");
        }

        public async Task<User> UpdateAsync(User user)
        {
            var result = await _userRepository.UpdateAsync(user);
            return result;
        }
    }
}
