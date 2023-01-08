using Core.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<RepositoryDbContext>(options =>
            options.UseInMemoryDatabase("UsersServiceDb"));
        
        services.AddScoped<IRepositoryDbContext>(provider =>
            provider.GetRequiredService<RepositoryDbContext>());
        
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}