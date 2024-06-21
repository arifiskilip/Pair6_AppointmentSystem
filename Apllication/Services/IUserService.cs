using Domain.Entities;

namespace Application.Services
{
    public interface IUserService
    {
        Task<User> UpdateAsync(User user);
        Task SetUserEmailVerified(int userId);
        Task<User> GetAuthenticatedUserAsync();
        Task<User> GetUserByEmailAsync(string email);
    }
}
