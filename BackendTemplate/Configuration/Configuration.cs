namespace BackendTemplate.Configuration;

public static class Configuration
{
    public static void Configure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<CadOptions>(config.GetSection("Cad"));

        JwtAuthorizationOptions.Initialize(config);
        JwtRefreshAuthorizationOptions.Initialize(config);
    }
}
