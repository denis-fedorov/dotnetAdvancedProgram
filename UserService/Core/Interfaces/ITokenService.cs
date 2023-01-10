using Core.Entities;

namespace Core.Interfaces;

public interface ITokenService
{
    public string? GenerateToken(User user);

    public bool IsTokenValid(string? token);
}