using BackendTemplate.Data.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace BackendTemplate.Database;

public class CadDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var server = Environment.GetEnvironmentVariable("POSTGRES_HOST");
        var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
        var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
        var database = Environment.GetEnvironmentVariable("POSTGRES_DB");

        var connectionString = $"Host={server};Username={user};Password={password};Database={database}";

        options.UseNpgsql(connectionString)
            .EnableDetailedErrors();
    }
}