namespace CartingService.Core.Exceptions;

public class CartNotFoundException : Exception
{
    public CartNotFoundException(string id)
        : base($"Cart with id {id} was not found")
    {
        // do nothing
    }
}