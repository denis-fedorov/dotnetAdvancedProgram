using Core.Interfaces;
using Core.Services;

namespace WebApi.Extensions;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection ConfigureWebApi(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();

        return services;
    }
}