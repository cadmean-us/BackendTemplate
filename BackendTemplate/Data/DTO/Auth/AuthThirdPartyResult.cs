using BackendTemplate.Services.Auth;

namespace BackendTemplate.Data.DTO.Auth;

public class AuthThirdPartyResult
{
    public AuthMethod Method { get; set; }
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
}