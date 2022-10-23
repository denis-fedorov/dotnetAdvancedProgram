using CartingService.Core.Entities;
using CartingService.Core.Interfaces;
using CartingService.SharedKernel;

namespace CartingService.Core.Services;

public class CartingService : ICartingService
{
    private readonly ICartingRepository _cartingRepository;

    public CartingService(ICartingRepository cartingRepository)
    {
        _cartingRepository = NullGuard.ThrowIfNull(cartingRepository);
    }

    public void Create(string id)
    {
        throw new NotImplementedException();
    }

    public Cart? Get(string id)
    {
        return _cartingRepository.Get(id);
    }
}