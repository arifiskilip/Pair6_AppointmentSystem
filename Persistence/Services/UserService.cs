using Application.Repositories;
using Application.Services;
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


        public async Task<User> UpdateAsync(User user)
        {
            var result = await _userRepository.UpdateAsync(user);
            return result;
        }
    }
}
