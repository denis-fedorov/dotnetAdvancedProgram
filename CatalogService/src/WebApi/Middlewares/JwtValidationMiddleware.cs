using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi.Interfaces;

namespace WebApi.Middlewares;

public class JwtValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITokenValidatorService _tokenValidatorService;

    public JwtValidationMiddleware(RequestDelegate next, ITokenValidatorService tokenValidatorService)
    {
        _next = next;
        _tokenValidatorService = tokenValidatorService;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            var claims = ExtractClaims(token);
            var issuer = claims.FirstOrDefault(c => c.Type == ClaimTypes.Authentication);

            var isIssuerSet = !string.IsNullOrWhiteSpace(issuer?.Value);
            if (isIssuerSet && await _tokenValidatorService.ValidateToken(token, issuer!.Value))
            {
                var identity = new ClaimsIdentity(claims, "basic");
                context.User = new ClaimsPrincipal(identity);
            }    
        }
        
        await _next(context);
    }

    private static List<Claim> ExtractClaims(string token)
    {
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