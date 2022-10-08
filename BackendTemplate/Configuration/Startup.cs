using Cadmean.RPC.ASP;
using BackendTemplate.Database;

namespace BackendTemplate.Configuration;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDbContext<CadDbContext>();
        services.ConfigureRpc();
        services.ConfigureCors();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseWebSockets();
        app.UseCors();
        app.UseRpc();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}