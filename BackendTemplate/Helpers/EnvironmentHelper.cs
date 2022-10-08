namespace BackendTemplate.Helpers;

public static class EnvironmentHelper
{
    public static bool IsDevelopment()
    {
        return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
    }
}