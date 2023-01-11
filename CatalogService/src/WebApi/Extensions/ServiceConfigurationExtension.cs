using WebApi.Interfaces;
using WebApi.Services;
using WebApi.Settings.Model;

namespace WebApi.Extensions;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection ConfigureWebApi(this IServiceCollection services)
    {
        services.AddSingleton<ITokenValidatorService, TokenValidatorService>();
        services.AddHttpClient();
        
        services.AddScoped<CategoryResourceFactory>();
        services.AddHttpContextAccessor();

        return services;
    }
}