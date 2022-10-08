namespace BackendTemplate.Configuration;

public static class CorsConfiguration
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(builder =>
        {
            builder.AddDefaultPolicy(policyBuilder =>
            {
                policyBuilder.AllowAnyOrigin();
                policyBuilder.AllowAnyHeader();
                policyBuilder.WithMethods("OPTIONS", "POST");
            });
        });
    }
}