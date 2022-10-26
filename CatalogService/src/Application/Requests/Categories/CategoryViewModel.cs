using Core.Entities;
using SharedKernel;

namespace Application.Requests.Categories;

public class CategoryViewModel
{
    public string Name { get; set; }

    public string? Image { get; set; }

    public string? ParentCategoryName { get; set; }

    public static CategoryViewModel FromEntity(Category category)
    {
        NullGuard.ThrowIfNull(category);
        
        return new CategoryViewModel
        {
            Name = category.Name,
            Image = category.Image,
            ParentCategoryName = category.ParentCategory?.Name
        };
    }
}