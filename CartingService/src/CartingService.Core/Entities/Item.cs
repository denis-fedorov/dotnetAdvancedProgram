using CartingService.Core.Exceptions;
using CartingService.SharedKernel;

namespace CartingService.Core.Entities;

public class Item : EntityBase
{
    public string Name { get; }
    public string? Image { get; }
    public decimal Price { get; }
    public uint Quantity { get; }

    public Item(string name, string? image, decimal price, uint quantity)
    {
        Name = NullGuard.ThrowIfNull(name);
        Image = NullGuard.ThrowIfNull(image);

        if (price <= 0)
        {
            throw new NonValidItemPriceException(price);
        }
        Price = price;

        if (quantity == 0)
        {
            throw new NonValidItemQuantityException(quantity);
        }
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"Item params: Id - '{Id}', Name - '{Name}', Image - '{Image}', Price - '{Price}', Quantity - '{Quantity}'";
    }
}