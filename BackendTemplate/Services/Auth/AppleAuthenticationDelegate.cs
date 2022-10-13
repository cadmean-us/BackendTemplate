using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using BackendTemplate.Data.DTO.Auth;
using Cadmean.CoreKit.Authentication;
using Cadmean.RPC;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BackendTemplate.Services.Auth;

public class AppleAuthenticationDelegate : IThirdPartyAuthenticationDelegate
{
    public async Task<AuthThirdPartyResult> Authenticate(AuthThirdPartyRequest request)
    {
        var secret = CreateSecretJwt();
        
        using var client = new HttpClient();
        
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", Environment.GetEnvironmentVariable("APPLE_BUNDLE_ID") },
            { "client_secret", secret },
            { "grant_type", "authorization_code" },
            { "code", request.Token }
        });
        content.Headers.Clear();
        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://appleid.apple.com/auth/token");
        httpRequest.Content = content;

        var httpResponse = await client.SendAsync(httpRequest);
        var json = await httpResponse.Content.ReadAsStringAsync();

        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        if (dictionary.ContainsKey("error"))
        {
            Console.WriteLine(dictionary["error"]);
            throw new FunctionException("apple_error");
        }
        
        var idToken = dictionary["id_token"] as string;
        var idJwt = new JwtToken(idToken);
        
        var appleId = idJwt.Claims.FirstOrDefault(c => c.Type == "sub")?.Value ?? "";
        var email = idJwt.Claims.FirstOrDefault(c => c.Type == "email")?.Value ?? "";

        return new AuthThirdPartyResult
        {
            Method = AuthMethod.Apple,
            UserId = appleId,
            Email = email,
            Name = ""
        };
    }

    private string CreateSecretJwt()
    {
        var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        var privateKey = Environment.GetEnvironmentVariable("APPLE_PRIVATE_KEY");
        ecdsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);
        var key = new ECDsaSecurityKey(ecdsa);
        var signingCredentials = new SigningCredentials(key, "ES256");

        var jwtHeader = new JwtHeader(signingCredentials);
        jwtHeader.Add("kid", Environment.GetEnvironmentVariable("APPLE_KID"));

        var iss = Environment.GetEnvironmentVariable("APPLE_TEAM_ID");
        var claims = new List<Claim>
        {
            new ("sub", Environment.GetEnvironmentVariable("APPLE_BUNDLE_ID")),
        };
        var jwtPayload = new JwtPayload(
            iss,
            "https://appleid.apple.com", 
            claims, 
            DateTime.Now, 
            DateTime.Now.Add(TimeSpan.FromMinutes(5)), 
            DateTime.Now
        );
        
        var jwt = new JwtSecurityToken(jwtHeader, jwtPayload);
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(jwt);
    }
}