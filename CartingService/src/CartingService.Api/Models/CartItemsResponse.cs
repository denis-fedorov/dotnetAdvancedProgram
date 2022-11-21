using CartingService.Core.Entities;

namespace CartingService.Api.Models;

public sealed class CartItemsResponse
{
    public IEnumerable<Item> Items { get; }

    public CartItemsResponse(IEnumerable<Item> items)
    {
        Items = items;
    }
}