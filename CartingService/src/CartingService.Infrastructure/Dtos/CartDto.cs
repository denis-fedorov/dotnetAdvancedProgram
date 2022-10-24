using CartingService.Core.Entities;

#pragma warning disable CS8618
#pragma warning disable CS86186

namespace CartingService.Infrastructure.Dtos;

public class CartDto
{
    public string Id { get; set; }
    public List<ItemDto> Items { get; set; }

    public CartDto()
    {
        // for deserialization purposes
    }

    public CartDto (Cart cart)
    {
        Id = cart.Id;
        Items = cart.Items
            .Select(i => new ItemDto(i))
            .ToList();
    }

    public Cart ToCart()
    {
        var cart = new Cart(Id);
        foreach (var item in Items)
        {
            cart.AddItem(item.ToItem());
        }
        
        return cart;
    }
}