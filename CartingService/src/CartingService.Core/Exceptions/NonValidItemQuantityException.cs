namespace CartingService.Core.Exceptions;

public class NonValidItemQuantityException : Exception
{
    public NonValidItemQuantityException(decimal price)
        : base($"Current quantity {price} is invalid")
    {
        // do nothing
    }
}