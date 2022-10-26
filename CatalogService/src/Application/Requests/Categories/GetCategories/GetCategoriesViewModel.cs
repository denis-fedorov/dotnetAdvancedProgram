using Core.Entities;
using SharedKernel;

namespace Application.Requests.Categories.GetCategories;

public sealed class GetCategoriesViewModel
{
    public IEnumerable<CategoriesTreeViewModel> Categories { get; set; }

    public static GetCategoriesViewModel FromEntities(IEnumerable<Category> categories)
    {
        categories = NullGuard.ThrowIfNull(categories).ToList();
        
        var lookup = categories
            .Select(c => new { Name = c.Name, ParentCategoryName = c.ParentCategory?.Name})
            .ToLookup(c => c.ParentCategoryName);
        var rawConvertedView = categories
            .Select(c => new CategoriesTreeViewModel
            {
                Name = c.Name,
                Image = c.Image,
                NestedCategories = null
            })
            .ToList();
        
        foreach (var view in rawConvertedView)
        {
            view.NestedCategories = rawConvertedView
                .Where(v => lookup[view.Name]
                    .Any(t => t.Name == v.Name))
                .ToList();
        }
        
        var result = rawConvertedView
            .Where(v => lookup[null]
                .Any(t => t.Name == v.Name))
            .ToList();

        return new GetCategoriesViewModel
        {
            Categories = result
        };
    }
}

public sealed class CategoriesTreeViewModel
{
    public string Name { get; set; }
    
    public string? Image { get; set; }

    public IEnumerable<CategoriesTreeViewModel>? NestedCategories { get; set; }
}