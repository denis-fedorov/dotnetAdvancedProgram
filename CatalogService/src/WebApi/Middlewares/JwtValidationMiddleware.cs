using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApi.Middlewares;

public class JwtValidationMiddleware
{
    private readonly RequestDelegate _next;

    public JwtValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        var claims = ExtractClaims(token);
        var issuer = claims.FirstOrDefault(c => c.Type == ClaimTypes.Authentication);
        
        if (issuer != null)
        {
            // TODO: call UserServiceAPI bu issuer URL for token validation
            //var isValid = UserServiceApi.ValidateToken(token);
            
            var identity = new ClaimsIdentity(claims, "basic");
            context.User = new ClaimsPrincipal(identity);
        }

        await _next(context);
    }

    private static List<Claim> ExtractClaims(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return new List<Claim>();
        }
        
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var claimsToken = jsonToken as JwtSecurityToken;

        var principalId = claimsToken?.Payload["sub"].ToString();
        var role = claimsToken?.Payload["role"].ToString();
        var issuer = claimsToken?.Payload["iss"].ToString();
        
        return new List<Claim>
        {
            new(ClaimTypes.Name, principalId),
            new(ClaimTypes.Role, role),
            new(ClaimTypes.Authentication, issuer)
        };
    }
}