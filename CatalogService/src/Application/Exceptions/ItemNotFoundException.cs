namespace Application.Exceptions;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string name)
        : base($"Item with name '{name}' was not found")
    {
        // do nothing
    }
}