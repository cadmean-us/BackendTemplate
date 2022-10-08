using BackendTemplate.Data.DTO;
using BackendTemplate.Data.DTO.Auth;
using BackendTemplate.Data.Entities.Auth;
using BackendTemplate.Database;
using BackendTemplate.Helpers;
using Cadmean.RPC.ASP;
using Cadmean.RPC.ASP.Attributes;
using Cadmean.RPC.ASP.Helpers;
using BC = BCrypt.Net.BCrypt;

namespace BackendTemplate.Controllers.Auth;

[FunctionRoute("auth.register")]
public class AuthRegisterController : FunctionController
{
    private readonly UnitOfWork unitOfWork = new();

    public async Task<EntityCreationResult> OnCall(RegistrationRequest request)
    {
        request.Validate();

        RpcAssert.IsTrue(!unitOfWork.UserRepository.CheckUserExistsByEmail(request.Email), "user_already_exists");
        
        var user = (User) request;
        user.Password = BC.HashPassword(user.Password);
        user.Roles.Add(UserRole.User);
        
        await unitOfWork.UserRepository.CreateAsync(user);
        await unitOfWork.SaveAsync();

        return new EntityCreationResult(user);
    }
}