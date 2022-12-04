using CartingService.Core.Exceptions;
using CartingService.SharedKernel;

namespace CartingService.Core.Entities;

public class Item : EntityBase
{
    public string Name { get; }
    public string? Image { get; }
    public decimal Price { get; private set; }
    public uint Quantity { get; }

    public Item(string id, string name, string? image, decimal price, uint quantity)
    {
        Id = NullGuard.ThrowIfNull(id);
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

    public void UpdatePrice(decimal price)
    {
        if (price <= 0)
        {
            throw new NonValidItemPriceException(price);
        }
        Price = price;
    }

    public override string ToString()
    {
        return $"Item params: Id - '{Id}', Name - '{Name}', Image - '{Image}', Price - '{Price}', Quantity - '{Quantity}'";
    }
}