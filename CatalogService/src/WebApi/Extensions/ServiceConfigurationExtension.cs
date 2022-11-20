using WebApi.Settings.Model;

namespace WebApi.Extensions;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection ConfigureWebApi(this IServiceCollection services)
    {
        services.AddScoped<CategoryResourceFactory>();
        services.AddHttpContextAccessor();
        
        return services;
    }
}