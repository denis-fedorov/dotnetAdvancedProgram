namespace Application.Exceptions;

public class RootCategoryDeleteException : Exception
{
    public RootCategoryDeleteException(string name)
        : base($"You cannot delete a category '{name}' because it has nested categories. Remove them first")
    {
        // do nothing
    }
}