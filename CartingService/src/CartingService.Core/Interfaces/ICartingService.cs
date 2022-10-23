using CartingService.Core.Entities;

namespace CartingService.Core.Interfaces;

public interface ICartingService
{
    public void Create(string id);

    public Cart? Get(string id);

    public void Delete(string id);
}