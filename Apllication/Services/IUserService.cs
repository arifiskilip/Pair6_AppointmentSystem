using Domain.Entities;

namespace Application.Services
{
    public interface IUserService
    {
        Task<User> UpdateAsync(User user);
    }
}
