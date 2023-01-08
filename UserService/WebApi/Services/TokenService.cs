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
    private readonly TokenSettings _tokenSettings;

    public TokenService(IOptions<TokenSettings> tokenSettings)
    {
        NullGuard.ThrowIfNull(tokenSettings);
        _tokenSettings = tokenSettings.Value;
    }

    public string? GenerateToken(User user)
    {
        NullGuard.ThrowIfNull(user);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, ((byte)user.Role).ToString()) }),
            Expires = DateTime.UtcNow.AddHours(_tokenSettings.ExpirationInHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public byte? ValidateToken(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, GetTokenValidationParameters(), out var validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;

            var role = byte.Parse(jwtToken.Claims.First(x => ClaimTypes.Role.Contains(x.Type)).Value);

            return role;
        }
        catch
        {
            return null;
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