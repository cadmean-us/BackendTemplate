using BackendTemplate.Helpers;
using Cadmean.CoreKit.Authentication;
using Cadmean.RPC.ASP;

namespace BackendTemplate.Configuration;

public static class RpcConfiguration
{
    public static void ConfigureRpc(this IServiceCollection services)
    {
        services.AddRpc(config =>
        {
            config.DebugMode = EnvironmentHelper.IsDevelopment();
            config.UseAuthorization(token =>
            {
                var jwt = new JwtToken(token);
                return jwt.Validate(JwtAuthorizationOptions.Current);
            });
        });
    }
}