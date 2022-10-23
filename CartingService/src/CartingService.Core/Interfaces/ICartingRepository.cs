using CartingService.Core.Entities;

namespace CartingService.Core.Interfaces;

public interface ICartingRepository
{
    public Cart? Get(string id);

    public void Add(Cart cart);

    public bool Exists(string id);
}