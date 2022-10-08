using BackendTemplate.Helpers;
using Cadmean.CoreKit.Authentication;

namespace BackendTemplate.Configuration;

public static class JwtAuthorizationOptions
{
    public static AuthOptions Current => EnvironmentHelper.IsDevelopment() ? development : production;

    private static readonly AuthOptions development = new(
        "https://vocably.cadmean.dev",
        "https://vocably.cadmean.dev",
        Environment.GetEnvironmentVariable("JWT_SECRET"),
        60
    );

    private static readonly AuthOptions production = new(
        "https://vocably.cadmean.dev",
        "https://vocably.cadmean.dev",
        Environment.GetEnvironmentVariable("JWT_SECRET"),
        60
    );
}