namespace WebApi.Interfaces;

public interface ITokenValidatorService
{
    public Task<bool> ValidateToken(string token, string host);
}