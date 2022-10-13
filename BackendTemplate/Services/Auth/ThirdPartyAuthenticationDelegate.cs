namespace BackendTemplate.Services.Auth;

public static class ThirdPartyAuthenticationDelegate
{
    public static IThirdPartyAuthenticationDelegate? Apple => new AppleAuthenticationDelegate();
    public static IThirdPartyAuthenticationDelegate? Google => new GoogleAuthenticationDelegate();

    public static IThirdPartyAuthenticationDelegate? ByName(string name)
    {
        return name switch
        {
            "Apple" => Apple,
            "Boogle" => Google,
            _ => null
        };
    }
}