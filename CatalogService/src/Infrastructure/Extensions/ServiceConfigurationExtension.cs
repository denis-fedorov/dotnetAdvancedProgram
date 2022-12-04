using Application.Interfaces;
using Infrastructure.Notification;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Infrastructure.Extensions;

public static class ServiceConfigurationExtension
{
    private const string RabbitMqSectionName = "RabbitMq";
    
    public static IServiceCollection ConfigureInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("CatalogServiceDb"));
        
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());
        
        var rabbitConfig = new RabbitMqConfig();
        configuration.Bind(RabbitMqSectionName, rabbitConfig);
        services.AddSingleton(rabbitConfig);
        services.AddSingleton(_ => new ConnectionFactory { HostName = rabbitConfig.Host });

        services.AddSingleton<INotificationService, NotificationService>();

        return services;
    }
}