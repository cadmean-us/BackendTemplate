using BackendTemplate.Data.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace BackendTemplate.Database;

public class CadDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public static string ConnectionString = "";

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(ConnectionString)
            .EnableDetailedErrors();
    }
}