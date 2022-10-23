namespace CartingService.Core.Exceptions;

public class NonValidItemPriceException : Exception
{
    public NonValidItemPriceException(decimal price)
        : base($"Current price {price} is invalid")
    {
        // do nothing
    }
}