namespace Application.Exceptions;

public class CategoryWithTheSameNameAlreadyExists : Exception
{
    public CategoryWithTheSameNameAlreadyExists(string name)
        : base($"Category with name '{name}' already exists")
    {
        // do nothing
    }
}