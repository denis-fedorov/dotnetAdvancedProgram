namespace Application.Exceptions;

public class ItemCategoryNotFoundException : Exception
{
    public ItemCategoryNotFoundException(string categoryName, string itemName)
        : base($"The category '{categoryName}' was not found for the item '{itemName}'")
    {
        // do nothing
    }
}