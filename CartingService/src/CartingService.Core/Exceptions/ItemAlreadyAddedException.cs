namespace CartingService.Core.Exceptions;

public class ItemAlreadyAddedException : Exception
{
    public ItemAlreadyAddedException(string itemId)
        : base($"Item with id '{itemId}' has already been added")
    {
        // do nothing
    }
}