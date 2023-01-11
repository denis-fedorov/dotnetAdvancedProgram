using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedKernel;
using WebApi.Configurations;

namespace WebApi.Services;

public class TokenService : ITokenService
{
    private const string PrincipalIdClaim = "sub";
    private const string IssuerClaim = "iss";
    
    private readonly TokenSettings _tokenSettings;

    public TokenService(IOptions<TokenSettings> tokenSettings)
    {
        NullGuard.ThrowIfNull(tokenSettings);
        _tokenSettings = tokenSettings.Value;
    }

    public string? GenerateToken(User user, string host)
    {
        NullGuard.ThrowIfNull(user);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(PrincipalIdClaim, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(IssuerClaim, host)
            }),
            Expires = DateTime.UtcNow.AddHours(_tokenSettings.ExpirationInHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool IsTokenValid(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, GetTokenValidationParameters(), out var validatedToken);
            
            return true;
        }
        catch
        {
            return false;
        }
    }

    private TokenValidationParameters GetTokenValidationParameters()
    {
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);

        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }
}