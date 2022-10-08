using BackendTemplate.Data.DTO.Auth;
using BackendTemplate.Database;
using Cadmean.RPC.ASP;
using Cadmean.RPC.ASP.Attributes;

namespace BackendTemplate.Controllers.Auth;

[RpcAuthorize]
[FunctionRoute("auth.info")]
public class AuthInfoController : FunctionController
{
    private readonly UnitOfWork unitOfWork = new();

    public async Task<UserDto> OnCall()
    {
        var user = await unitOfWork.UserRepository.FindByAuthAsync(Call.Authorization);
        return (UserDto) user;
    }
}