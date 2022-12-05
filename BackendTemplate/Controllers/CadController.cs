namespace BackendTemplate.Controllers;

public class CadController
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