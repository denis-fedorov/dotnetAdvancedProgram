using System.Reflection;
using CartingService.Core.Interfaces;
using Microsoft.OpenApi.Models;

namespace CartingService.Api.Extensions;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Carts API",
                Description = "Carts API sample project",
                Contact = new OpenApiContact
                {
                    Name = "Denis Fedorov"
                }
            });
    
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        
        services.AddScoped<ICartingService, Core.Services.CartingService>();

        return services;
    }
}