using System.Collections.ObjectModel;
using System.Text;
using CartingService.Core.Exceptions;
using CartingService.SharedKernel;

namespace CartingService.Core.Entities;

public class Cart : EntityBase
{
    private readonly List<Item> _items;
    public ReadOnlyCollection<Item> Items => _items.AsReadOnly();

    public Cart(string id)
    {
        Id = NullGuard.ThrowIfNull(id);
        _items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        NullGuard.ThrowIfNull(item);
        
        var itemId = item.Id;
        if (_items.Any(i => i.Id == itemId))
        {
            throw new ItemAlreadyAddedException(itemId);
        }
        
        _items.Add(item);
    }

    public void RemoveItem(string itemId)
    {
        NullGuard.ThrowIfNull(itemId);
        
        var itemToRemove = _items.SingleOrDefault(i => i.Id == itemId);
        if (itemToRemove is null)
        {
            throw new RemoveNonAddedItemException(itemId);
        }

        _items.Remove(itemToRemove);
    }

    public void ClearCart()
    {
        _items.Clear();
    }

    public override string ToString()
    {
        var allItems = new StringBuilder($"Cart with id '{Id}' has:{Environment.NewLine}");
        foreach (var item in _items)
        {
            allItems.AppendLine(item.ToString());
        }
        
        return allItems.ToString();
    }
}