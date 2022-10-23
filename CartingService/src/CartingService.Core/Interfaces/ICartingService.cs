using CartingService.Core.Entities;

namespace CartingService.Core.Interfaces;

public interface ICartingService
{
    public void Create(string id);

    public Cart? Get(string id);

    public void Delete(string id);
    
    public void CreateItem(string cartId, Item item);

    public Item? GetItem(string cartId, string itemId);
    
    public void DeleteItem(string cartId, string itemId);
}