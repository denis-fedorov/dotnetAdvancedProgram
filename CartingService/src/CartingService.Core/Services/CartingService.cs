using CartingService.Core.Entities;
using CartingService.Core.Exceptions;
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

    public Cart? Get(string id)
    {
        NullGuard.ThrowIfNull(id);
        
        return _cartingRepository.Get(id);
    }

    public void PutItem(string cartId, Item item)
    {
        NullGuard.ThrowIfNull(cartId);
        NullGuard.ThrowIfNull(item);
        
        if (!_cartingRepository.Exists(cartId))
        {
            var newCart = new Cart(cartId);
            _cartingRepository.Create(newCart);
        }
        var cart = _cartingRepository.Get(cartId)!;
        
        cart.AddItem(item);
        _cartingRepository.Update(cart);    
    }

    public void DeleteItem(string cartId, string itemId)
    {
        NullGuard.ThrowIfNull(cartId);
        NullGuard.ThrowIfNull(itemId);
        
        var cart = _cartingRepository.Get(cartId);
        if (cart is null)
        {
            throw new CartNotFoundException(cartId);
        }
        
        cart.RemoveItem(itemId);
        _cartingRepository.Update(cart);    
    }
}