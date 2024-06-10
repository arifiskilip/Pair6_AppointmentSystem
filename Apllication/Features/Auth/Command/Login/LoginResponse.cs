using Core.Security.JWT;

namespace Application.Features.Auth.Command.Login
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public AccessToken AccessToken { get; set; }
    }
}
