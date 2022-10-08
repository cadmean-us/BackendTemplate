using BackendTemplate.Helpers;
using Cadmean.CoreKit.Authentication;

namespace BackendTemplate.Configuration;

public static class JwtRefreshAuthorizationOptions
{
    public static AuthOptions Current => EnvironmentHelper.IsDevelopment() ? development : production;

    private static readonly AuthOptions development = new(
        "https://ari.cadmean.dev",
        "https://ari.cadmean.dev",
        Environment.GetEnvironmentVariable("JWT_SECRET"),
        43200
    );

    private static readonly AuthOptions production = new(
        "https://ari.cadmean.dev",
        "https://ari.cadmean.dev",
        Environment.GetEnvironmentVariable("JWT_SECRET"),
        43200
    );
}