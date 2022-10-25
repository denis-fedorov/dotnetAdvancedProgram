using Core.Exceptions;
using SharedKernel;

namespace Core.Entities;

public sealed class Category : EntityBase
{
    public const int NameMaxLength = 50;
    
    public string Name { get; private set; }

    public string? Image { get; private set; }

    public Category? ParentCategory { get; private set; }
    
    protected Category()
    { }

    public Category(string name, string? image, Category? parentCategory)
    {
        Name = NullGuard.ThrowIfNull(name);
        if (Name.Length >= NameMaxLength)
        {
            throw new NameTooLongException(Name, NameMaxLength);
        }
        
        Image = image;
        ParentCategory = parentCategory;
    }
}