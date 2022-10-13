using BackendTemplate.Data.DTO.Auth;
using BackendTemplate.Exceptions;
using Google.Apis.Auth;

namespace BackendTemplate.Services.Auth;

public class GoogleAuthenticationDelegate : IThirdPartyAuthenticationDelegate
{
    public async Task<AuthThirdPartyResult> Authenticate(AuthThirdPartyRequest request)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token);
            return new AuthThirdPartyResult
            {
                Method = AuthMethod.Google,
                Email = payload.Email,
                Name = payload.Name ?? "",
                UserId = payload.Subject
            };
        }
        catch (InvalidJwtException)
        {
            throw new CadException("google_error");
        }
    }
}