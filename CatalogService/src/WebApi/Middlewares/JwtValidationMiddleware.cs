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
        
        // TODO: call UserServiceAPI for token validation
        //var isValid = UserServiceApi.ValidateToken(token);
        if (token != null)
        {
            var claims = ExtractClaims(token);
            var identity = new ClaimsIdentity(claims, "basic");
            context.User = new ClaimsPrincipal(identity);
        }

        await _next(context);
    }

    private static IEnumerable<Claim> ExtractClaims(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var claimsToken = jsonToken as JwtSecurityToken;

        var principalId = claimsToken?.Payload["sub"].ToString();
        var role = claimsToken?.Payload["role"].ToString();
        
        return new[]
        {
            new Claim(ClaimTypes.Name, principalId),
            new Claim(ClaimTypes.Role, role)
        };
    }
}