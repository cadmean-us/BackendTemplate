using BackendTemplate.Database;

namespace BackendTemplate.Configuration;

public static class DbContextConfiguration
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        CadDbContext.ConnectionString = configuration["Haze:ConnectionString"] ??
            throw new Exception("No connection string");

        services.AddDbContext<CadDbContext>();
    }
}