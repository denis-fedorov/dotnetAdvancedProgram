﻿using CartingService.Core.Entities;
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

    public void Create(string id)
    {
        NullGuard.ThrowIfNull(id);
        if (_cartingRepository.Exists(id))
        {
            throw new CartAlreadyCreatedException(id);
        }

        var cart = new Cart(id);
        _cartingRepository.Create(cart);
    }

    public Cart? Get(string id)
    {
        NullGuard.ThrowIfNull(id);
        
        return _cartingRepository.Get(id);
    }

    public void Delete(string id)
    {
        NullGuard.ThrowIfNull(id);
        
        _cartingRepository.Delete(id);
    }

    public Item? GetItem(string cartId, string itemId)
    {
        NullGuard.ThrowIfNull(cartId);
        NullGuard.ThrowIfNull(itemId);

        return _cartingRepository.GetItem(cartId, itemId);
    }
}