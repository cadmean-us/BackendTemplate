using BackendTemplate.Data.DTO.Auth;

namespace BackendTemplate.Services.Auth;

public interface IThirdPartyAuthenticationDelegate
{
    Task<AuthThirdPartyResult> Authenticate(AuthThirdPartyRequest request);
}