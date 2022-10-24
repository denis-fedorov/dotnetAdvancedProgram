namespace Core.Exceptions;

public class NonValidItemAmountException : Exception
{
    public NonValidItemAmountException(uint amount)
        : base($"Current amount {amount} is invalid")
    {
        // do nothing
    }
}