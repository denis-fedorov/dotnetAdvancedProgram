using Core.Entities;
using SharedKernel;

namespace Application.Requests.Items.GetItems;

public class ItemViewModel
{
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Image { get; set; }
    
    public string CategoryName { get; set; }
    
    public decimal Price { get; set; }
    
    public uint Amount { get; set; }

    public static ItemViewModel FromEntity(Item item)
    {
        NullGuard.ThrowIfNull(item);

        return new ItemViewModel
        {
            Name = item.Name,
            Description = item.Description,
            Image = item.Image,
            CategoryName = item.Category.Name,
            Price = item.Price,
            Amount = item.Amount
        };
    }
}