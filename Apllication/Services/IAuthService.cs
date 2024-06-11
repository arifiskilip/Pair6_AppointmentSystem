namespace Application.Services
{
    public interface IAuthService
    {
        Task<int> GetAuthenticatedUserIdAsync();
    }
}
