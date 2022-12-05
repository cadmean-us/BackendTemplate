using BackendTemplate.Services.Auth;

namespace BackendTemplate.Data.DTO.Auth;

public class AuthThirdPartyResult
{
    public AuthMethod Method { get; set; }
    public required string UserId { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public int AccessTokenExpiresIn { get; set; }
}