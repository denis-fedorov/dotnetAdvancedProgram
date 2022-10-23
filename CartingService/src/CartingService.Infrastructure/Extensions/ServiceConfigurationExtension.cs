using CartingService.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CartingService.Infrastructure.Extensions;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICartingRepository, CartingRepository>();

        return services;
    }
}