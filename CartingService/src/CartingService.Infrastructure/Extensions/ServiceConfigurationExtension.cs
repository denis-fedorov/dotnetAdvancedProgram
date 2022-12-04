using CartingService.Core.Interfaces;
using CartingService.Infrastructure.Notification;
using CartingService.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace CartingService.Infrastructure.Extensions;

public static class ServiceConfigurationExtension
{
    private const string RabbitMqSectionName = "RabbitMq";
    
    public static void ConfigureInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddScoped<ICartingRepository, CartingRepository>();
        services.Configure<ConnectionSettings>(options =>
            options.ConnectionString = configuration
                .GetConnectionString(ConnectionSettings.ConnectionName)
        );
        
        var rabbitConfig = new RabbitMqConfig();
        configuration.Bind(RabbitMqSectionName, rabbitConfig);
        services.AddSingleton(rabbitConfig);
        services.AddSingleton(_ => new ConnectionFactory { HostName = rabbitConfig.Host });

        services.AddHostedService<NotificationService>();
    }
}