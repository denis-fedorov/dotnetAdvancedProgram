using SharedKernel;

namespace Core.Entities;

public sealed class Category : EntityBase
{
    public string Name { get; private set; }

    public string? Image { get; private set; }

    public Category? ParentCategory { get; private set; }
    
    public Category(string name, string? image, Category parentCategory)
    {
        Name = NullGuard.ThrowIfNull(name);
        Image = image;
        ParentCategory = parentCategory;
    }
}