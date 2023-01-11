using WebApi.Interfaces;

namespace WebApi.Services;

public class TokenValidatorService : ITokenValidatorService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string ValidationEndpoint = "users/validate"; 

    public TokenValidatorService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> ValidateToken(string token, string host)
    {
        try
        {
            var client = CreateClient(host);

            var response = await client.PostAsJsonAsync(ValidationEndpoint, new { token } );
            
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    private HttpClient CreateClient(string baseAddress)
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(baseAddress);

        return client;
    }
}