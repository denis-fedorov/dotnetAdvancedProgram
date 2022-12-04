using Core.Exceptions;
using SharedKernel;

namespace Core.Entities;

public sealed class Item : EntityBase
{
    public const int NameMaxLength = 50;
    
    public string Name { get; private set; }
    
    public string? Description { get; private set; }
    
    public string? Image { get; private set; }
    
    public Category Category { get; private set; }
    
    public decimal Price { get; private set; }
    
    public uint Amount { get; private set; }

    private Item()
    {
        // hack
    }

    public Item(string name, string? description, string? image, Category category, decimal price, uint amount)
    {
        Name = NullGuard.ThrowIfNull(name);
        if (Name.Length >= NameMaxLength)
        {
            throw new NameTooLongException(Name, NameMaxLength);
        }
        
        Description = description;
        Image = image;
        Category = NullGuard.ThrowIfNull(category);

        if (price <= 0)
        {
            throw new NonValidItemPriceException(price);
        }
        Price = price;

        if (amount == 0)
        {
            throw new NonValidItemAmountException(amount);
        }
        Amount = amount;
    }

    public bool TryUpdatePrice(decimal newPrice)
    {
        if (Price == newPrice)
        {
            return false;
        }
        
        if (newPrice <= 0)
        {
            throw new NonValidItemPriceException(newPrice);
        }

        Price = newPrice;

        return true;
    }
}