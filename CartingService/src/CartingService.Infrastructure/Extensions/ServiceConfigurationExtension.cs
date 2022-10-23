﻿using CartingService.Core.Interfaces;
using CartingService.Infrastructure.Settings;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CartingService.Infrastructure.Extensions;

public static class ServiceConfigurationExtension
{
    public static void ConfigureInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddScoped<ICartingRepository, CartingRepository>();
        services.Configure<ConnectionSettings>(options =>
            options.ConnectionString = configuration
                .GetConnectionString(ConnectionSettings.ConnectionName)
        );
        
        BsonMapper.Global.UpdateDbMappings();
    }
}