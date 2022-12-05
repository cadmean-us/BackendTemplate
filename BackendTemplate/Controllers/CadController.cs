using BackendTemplate.Data.Entities.Auth;
using BackendTemplate.Database;
using BackendTemplate.Helpers;
using Cadmean.RPC.ASP;

namespace BackendTemplate.Controllers;

public class CadController : FunctionController
{
    protected readonly UnitOfWork UnitOfWork = new();

    protected Task<User> FindUser()
    {
        return UnitOfWork.UserRepository.FindByAuthAsync(Call.Authorization);
    }

    protected int UserId
    {
        get
        {
            var claims = JwtHelper.GetTokenClaims(Call.Authorization);
            return claims.UserId;
        }
    }
}