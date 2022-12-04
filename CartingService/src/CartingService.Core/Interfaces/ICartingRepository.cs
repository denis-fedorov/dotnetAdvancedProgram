using CartingService.Core.Entities;

namespace CartingService.Core.Interfaces;

public interface ICartingRepository
{
    public Cart? Get(string id);

    public void Create(Cart cart);

    public bool Exists(string id);

    public void Update(Cart cart);

    public void UpdateItemPrice(string itemId, decimal price);
}