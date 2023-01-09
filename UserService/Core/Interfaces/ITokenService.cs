using Core.Entities;

namespace Core.Interfaces;

public interface ITokenService
{
    public string? GenerateToken(User user);

    public string? ValidateToken(string? token);
}