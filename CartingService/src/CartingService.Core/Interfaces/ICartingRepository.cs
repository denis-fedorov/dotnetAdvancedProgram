using CartingService.Core.Entities;

namespace CartingService.Core.Interfaces;

public interface ICartingRepository
{
    public Cart Get(string id);

    public void Save(Cart cart);
}