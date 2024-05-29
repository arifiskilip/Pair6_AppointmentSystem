using Core.Security.Entitites;

namespace Core.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(BaseUser user, IList<OperationClaim> operationClaims);

        // RefreshToken CreateRefreshToken(User user, string ipAddress);
    }
}
