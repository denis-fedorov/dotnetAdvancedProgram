using Core.Entities;

namespace Application.Interfaces;

public interface INotificationService
{
    public void SendNewPriceMessage(Item item);
}