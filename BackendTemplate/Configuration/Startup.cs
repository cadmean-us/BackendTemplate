using Cadmean.RPC.ASP;

namespace BackendTemplate.Configuration;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.ConfigureDbContext(configuration);
        services.ConfigureRpc();
        services.ConfigureCors();
        services.Configure(configuration);
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