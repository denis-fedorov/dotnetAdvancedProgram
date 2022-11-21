using System.Reflection;
using CartingService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

namespace CartingService.Api.Extensions;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.ResolveConflictingActions (apiDescriptions => apiDescriptions.First ());
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
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "Carts API updated",
                Description = "Carts API sample project",
                Contact = new OpenApiContact
                {
                    Name = "Denis Fedorov"
                }
            });
    
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new HeaderApiVersionReader("X-Version"),
                new MediaTypeApiVersionReader("ver"));
        });

        services.AddScoped<ICartingService, Core.Services.CartingService>();

        return services;
    }
}