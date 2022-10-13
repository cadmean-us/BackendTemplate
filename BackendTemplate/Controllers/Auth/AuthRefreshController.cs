using BackendTemplate.Configuration;
using BackendTemplate.Data.DTO.Auth;
using BackendTemplate.Database;
using BackendTemplate.Helpers;
using Cadmean.CoreKit.Authentication;
using Cadmean.RPC;
using Cadmean.RPC.ASP;
using Cadmean.RPC.ASP.Attributes;
using Cadmean.RPC.ASP.Helpers;

namespace BackendTemplate.Controllers.Auth;

[FunctionRoute("auth.refresh")]
public class AuthRefreshController : FunctionController
{
    private readonly UnitOfWork unitOfWork = new();

    public async Task<JwtAuthorizationTicket> OnCall(RefreshRequest request)
    {
        request.Validate();

        var user = unitOfWork.UserRepository.FindById(request.UserId);

        var jwt = new JwtToken(request.RefreshToken);
        RpcAssert.IsTrue(jwt.Validate(JwtRefreshAuthorizationOptions.Current), "invalid_refresh_token");
        RpcAssert.IsTrue(user.RefreshToken == request.RefreshToken, "invalid_refresh_token");

        var ticket = JwtHelper.CreateTicket(user);

        user.RefreshToken = ticket.RefreshToken;
        unitOfWork.UserRepository.Update(user);
        await unitOfWork.SaveAsync();

        return ticket;
    }
}