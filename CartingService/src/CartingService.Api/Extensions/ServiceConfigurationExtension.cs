﻿using CartingService.Core.Interfaces;

namespace CartingService.Api.Extensions;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<ICartingService, Core.Services.CartingService>();

        return services;
    }
}