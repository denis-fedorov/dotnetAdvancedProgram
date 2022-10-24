namespace CartingService.Core.Exceptions;

public class RemoveNonAddedItemException : Exception
{
    public RemoveNonAddedItemException(string itemId)
        : base($"Remove a non-added item with id '{itemId}'")
    {
        // do nothing
    }
}