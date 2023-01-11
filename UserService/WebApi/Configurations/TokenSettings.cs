namespace WebApi.Configurations;

public class TokenSettings
{
    public string Secret { get; set; }

    public byte ExpirationInHours { get; set; }
}