using BackendTemplate.Data.DTO.Auth;
using BackendTemplate.Data.Entities.Auth;
using BackendTemplate.Database;
using BackendTemplate.Exceptions;
using BackendTemplate.Helpers;
using BackendTemplate.Services.Auth;
using Cadmean.RPC;
using Cadmean.RPC.ASP;
using Cadmean.RPC.ASP.Attributes;
using Cadmean.RPC.ASP.Helpers;

namespace BackendTemplate.Controllers.Auth;

[FunctionRoute("auth.thirdParty")]
public class ThirdPartyAuthenticationController : FunctionController
{
    private readonly UnitOfWork unitOfWork = new();

    public async Task<JwtAuthorizationTicket> OnCall(AuthThirdPartyRequest request)
    {
        request.Validate();
        
        var authDelegate = ThirdPartyAuthenticationDelegate.ByName(request.Provider);
        RpcAssert.IsTrue(authDelegate != null, "auth_provider_not_found");

        var auth = await authDelegate!.Authenticate(request);
        var user = FindUserWithProviderId(auth) ?? (FindUserByProviderEmail(auth) ?? await CreateUser(auth));

        await unitOfWork.SaveAsync();
        
        var ticket = JwtHelper.CreateTicket(user);
        user.RefreshToken = ticket.RefreshToken;
        unitOfWork.UserRepository.Update(user);

        await unitOfWork.SaveAsync();

        return ticket;
    }

    private User? FindUserWithProviderId(AuthThirdPartyResult auth)
    {
        var users = auth.Method switch
        {
            AuthMethod.Apple => unitOfWork.UserRepository.Find(u => u.AppleId == auth.UserId),
            AuthMethod.Google => unitOfWork.UserRepository.Find(u => u.GoogleId == auth.UserId),
            _ => throw new CadException("auth_provider_not_found")
        };

        return users.FirstOrDefault();
    }

    private User? FindUserByProviderEmail(AuthThirdPartyResult auth)
    {
        if (!unitOfWork.UserRepository.CheckUserExistsByEmail(auth.Email))
        {
            return null;
        }

        return unitOfWork.UserRepository.FindByEmail(auth.Email);
    }

    private async Task<User> CreateUser(AuthThirdPartyResult auth)
    {
        var user = new User
        {
            FirstName = auth.Name,
            LastName = "",
            Email = auth.Email,
            Status = UserStatus.Active,
            Roles = new List<UserRole> { UserRole.User },
        };
        switch (auth.Method)
        {
            case AuthMethod.Apple:
                user.AppleId = auth.UserId;
                break;
            case AuthMethod.Google:
                user.GoogleId = auth.UserId;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        await unitOfWork.UserRepository.CreateAsync(user);
        return user;
    }
}