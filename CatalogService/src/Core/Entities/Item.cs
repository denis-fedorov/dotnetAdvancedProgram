using Core.Exceptions;
using SharedKernel;

namespace Core.Entities;

public sealed class Item : EntityBase
{
    public string Name { get; private set; }
    
    public string? Description { get; private set; }
    
    public string? Image { get; private set; }
    
    public Category Category { get; private set; }
    
    public decimal Price { get; private set; }
    
    public uint Amount { get; private set; }
    
    public Item(string name, string? description, string? image, Category category, decimal price, uint amount)
    {
        Name = NullGuard.ThrowIfNull(name);
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
}