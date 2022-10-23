using CartingService.Core.Entities;
using CartingService.Core.Interfaces;
using CartingService.Infrastructure.Settings;
using CartingService.SharedKernel;
using Microsoft.Extensions.Options;

namespace CartingService.Infrastructure;

public class CartingRepository : ICartingRepository
{
    private readonly string? _connectionString;
    
    public CartingRepository(IOptions<ConnectionSettings> connectionSettings)
    {
        NullGuard.ThrowIfNull(connectionSettings);

        var connectionString = connectionSettings.Value.ConnectionString;
        _connectionString = NullGuard.ThrowIfNull(connectionString);
    }
    
    public Cart? Get(string id)
    {
        return null;
    }

    public void Save(Cart cart)
    {
        throw new NotImplementedException();
    }
}