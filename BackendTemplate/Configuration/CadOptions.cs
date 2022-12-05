namespace BackendTemplate.Configuration;

public class CadOptions
{
    public string JwtSecret { get; set; } = "";
    public string ConnectionString { get; set; } = "";
    public string FrontendBaseUrl { get; set; } = "";
}
