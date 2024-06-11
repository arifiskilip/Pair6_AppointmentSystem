using Application.Services;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.Security.Extensions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContext;

        public AuthService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public async Task<int> GetAuthenticatedUserIdAsync()
        {
            string? id = _httpContext?.HttpContext?.User?.Claims(ClaimTypes.NameIdentifier)?.FirstOrDefault();
            if (id is null) throw new BusinessException("Kullanıcı bulunamadı!");

            return Convert.ToInt32(id);
        }
    }
}
