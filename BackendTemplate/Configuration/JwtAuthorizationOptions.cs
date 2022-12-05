using BackendTemplate.Helpers;
using Cadmean.CoreKit.Authentication;

namespace BackendTemplate.Configuration;

public static class JwtAuthorizationOptions
{
    public static AuthOptions Current { get; private set; }

    public static void Initialize(IConfiguration configuration)
    {
        var jwtSecret = configuration["Haze:JwtSecret"];
        var issuer = EnvironmentHelper.IsDevelopment() ? "https://hazedev.cadmean.dev" : "https://haze.cadmean.dev";
        var lifetime = EnvironmentHelper.IsDevelopment() ? 1440 : 30;
        Current = new AuthOptions(issuer, issuer, jwtSecret, lifetime);
    }
}