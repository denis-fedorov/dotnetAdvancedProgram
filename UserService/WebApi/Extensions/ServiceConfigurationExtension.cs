using Core.Interfaces;
using Core.Services;
using WebApi.Configurations;
using WebApi.Services;

namespace WebApi.Extensions;

public static class ServiceConfigurationExtension
{
    private const string TokenSettingsSectionName = "TokenSettings";
    
    public static IServiceCollection ConfigureWebApi(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<ITokenService, TokenService>();

        services.Configure<TokenSettings>(configuration.GetSection(TokenSettingsSectionName));

        return services;
    }
}