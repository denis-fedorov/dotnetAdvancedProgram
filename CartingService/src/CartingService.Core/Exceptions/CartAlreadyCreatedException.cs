namespace CartingService.Core.Exceptions;

public class CartAlreadyCreatedException : Exception
{
    public CartAlreadyCreatedException(string id)
        : base($"A cart with id '{id}' has already created")
    {
        // do nothing
    }
}