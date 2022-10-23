using CartingService.Core.Interfaces;
using CartingService.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CartingService.Infrastructure.Extensions;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection ConfigureInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddScoped<ICartingRepository, CartingRepository>();
        services.Configure<ConnectionSettings>(options =>
            options.ConnectionString = configuration
                .GetConnectionString(ConnectionSettings.ConnectionName)
        );

        return services;
    }
}