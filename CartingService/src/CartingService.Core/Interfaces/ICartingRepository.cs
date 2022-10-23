using CartingService.Core.Entities;

namespace CartingService.Core.Interfaces;

public interface ICartingRepository
{
    public Cart? Get(string id);

    public void Create(Cart cart);

    public bool Exists(string id);
    
    public void Delete(string id);

    public Item? GetItem(string cartId, string itemId);
    
    public void CreateItem(string cartId, Item item);

    public void DeleteItem(string cartId, string itemId);
}