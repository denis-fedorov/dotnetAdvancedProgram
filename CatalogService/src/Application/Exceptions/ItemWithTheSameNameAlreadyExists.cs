namespace Application.Exceptions;

public class ItemWithTheSameNameAlreadyExists : Exception
{
    public ItemWithTheSameNameAlreadyExists(string name)
        : base($"Item with name '{name}' already exists")
    {
        // do nothing
    }
}