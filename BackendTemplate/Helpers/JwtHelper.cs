using System.Security.Claims;
using BackendTemplate.Configuration;
using BackendTemplate.Data.Entities.Auth;
using Cadmean.CoreKit.Authentication;
using Cadmean.RPC;

namespace BackendTemplate.Helpers;

public static class JwtHelper
{
    public static JwtAuthorizationTicket CreateTicket(User user)
    {
        var accessToken = new JwtToken(
            JwtAuthorizationOptions.Current, 
            new List<Claim>
            {
                new("userId", user.Id.ToString()),
                // new("sessionId", session.SessionId),
            }, 
            "ari"
        );
        
        var refreshToken = new JwtToken(
            JwtRefreshAuthorizationOptions.Current, 
            new List<Claim>
            {
                new("userId", user.Id.ToString()),
                // new("sessionId", session.SessionId),
            }, 
            "ari"
        );

        return new JwtAuthorizationTicket(accessToken.ToString(), refreshToken.ToString());
    }
    
    public static JwtTokenClaims GetTokenClaims(string token)
    {
        var jwt = new JwtToken(token);
        return new JwtTokenClaims
        {
            UserId = int.Parse(jwt.Claims.First(c => c.Type == "userId").Value),
            // SessionId = jwt.Claims.First(c => c.Type == "sessionId").Value,
        };
    }
    
    public struct JwtTokenClaims
    {
        public int UserId;
        public string SessionId;
    }
}