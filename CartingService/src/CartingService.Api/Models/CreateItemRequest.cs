using CartingService.Core.Entities;

namespace CartingService.Api.Models;

public class CreateItemRequest
{
    public string Name { get; set; }
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public uint Quantity { get; set; }

    public Item ToItem(string id)
    {
        return new Item(id: id, name: Name, image: Image, price: Price, quantity: Quantity);
    }
}