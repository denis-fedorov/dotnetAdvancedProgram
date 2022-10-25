namespace Application.Exceptions;

public class ParentCategoryNotFoundException : Exception
{
    public ParentCategoryNotFoundException(string parentCategoryName)
        : base($"Parent category '{parentCategoryName}' was not found")
    {
        // do nothing
    }
}