using CartingService.Core.Entities;

#pragma warning disable CS8618

namespace CartingService.Infrastructure.Dtos;

public class ItemDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Image { get; set; }
    public decimal Price { get; set; }
    public uint Quantity { get; set; }

    public ItemDto()
    {
        // for deserialization purposes
    }

    public ItemDto(Item item)
    {
        Id = item.Id;
        Name = item.Name;
        Image = item.Image;
        Price = item.Price;
        Quantity = item.Quantity;
    }

    public Item ToItem()
    {
        return new Item(Id, Name, Image, Price, Quantity);
    }
}