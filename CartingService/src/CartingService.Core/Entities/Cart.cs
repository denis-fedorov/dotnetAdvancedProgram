using System.Text;
using CartingService.Core.Exceptions;
using CartingService.SharedKernel;

namespace CartingService.Core.Entities;

public class Cart : EntityBase
{
    public IList<Item> Items { get; private set; } = new List<Item>();

    public Cart(string id)
    {
        Id = NullGuard.ThrowIfNull(id);
    }

    public void AddItem(Item item)
    {
        NullGuard.ThrowIfNull(item);
        
        var itemId = item.Id;
        if (Items.Any(i => i.Id == itemId))
        {
            throw new ItemAlreadyAddedException(itemId);
        }
        
        Items.Add(item);
    }

    public void RemoveItem(string itemId)
    {
        NullGuard.ThrowIfNull(itemId);
        
        var itemToRemove = Items.SingleOrDefault(i => i.Id == itemId);
        if (itemToRemove is null)
        {
            throw new RemoveNonAddedItemException(itemId);
        }

        Items.Remove(itemToRemove);
    }

    public void UpdateItemPrice(string itemId, decimal price)
    {
        NullGuard.ThrowIfNull(itemId);
        
        var itemToUpdate = Items.SingleOrDefault(i => i.Id == itemId);
        if (itemToUpdate is null)
        {
            throw new RemoveNonAddedItemException(itemId);
        }
        
        itemToUpdate.UpdatePrice(price);
    }

    public override string ToString()
    {
        var allItems = new StringBuilder($"Cart with id '{Id}' has:{Environment.NewLine}");
        foreach (var item in Items)
        {
            allItems.AppendLine(item.ToString());
        }
        
        return allItems.ToString();
    }
}