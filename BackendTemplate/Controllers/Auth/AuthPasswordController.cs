using BackendTemplate.Data.DTO.Auth;
using BackendTemplate.Database;
using BackendTemplate.Helpers;
using Cadmean.RPC;
using Cadmean.RPC.ASP;
using Cadmean.RPC.ASP.Attributes;
using Cadmean.RPC.ASP.Helpers;
using BC = BCrypt.Net.BCrypt;

namespace BackendTemplate.Controllers.Auth;

[FunctionRoute("auth.password")]
public class AuthPasswordController : FunctionController
{
    private readonly UnitOfWork unitOfWork = new();
    
    public async Task<JwtAuthorizationTicket> OnCall(AuthPasswordRequest request)
    {
        request.Validate();
        
        var user = unitOfWork.UserRepository.FindByEmail(request.Email);

        RpcAssert.IsTrue(!string.IsNullOrEmpty(user.Password), "no_password");
        RpcAssert.IsTrue(BC.Verify(request.Password, user.Password), "wrong_password");

        var ticket = JwtHelper.CreateTicket(user);

        user.RefreshToken = ticket.RefreshToken;
        await unitOfWork.SaveAsync();

        return ticket;
    }
}