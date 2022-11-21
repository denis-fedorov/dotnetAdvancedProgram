using CartingService.Core.Entities;

namespace CartingService.Core.Interfaces;

public interface ICartingService
{
    public Cart? Get(string id);

    public void PutItem(string cartId, Item item);

    public void DeleteItem(string cartId, string itemId);
}