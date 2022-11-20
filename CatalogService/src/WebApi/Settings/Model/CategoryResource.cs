using Application.Requests.Categories;
using SharedKernel;

namespace WebApi.Settings.Model;

public sealed class CategoryResource : ResourceBase
{
    public string Name { get; }

    public string? Image { get; }

    public string? ParentCategoryName { get; }
    
    public CategoryResource(
        LinkGenerator linkGenerator,
        IHttpContextAccessor httpContextAccessor,
        CategoryViewModel categoryViewModel)
            : base(linkGenerator, httpContextAccessor)
    {
        NullGuard.ThrowIfNull(categoryViewModel);

        Name = categoryViewModel.Name;
        Image = categoryViewModel.Image;
        ParentCategoryName = categoryViewModel.ParentCategoryName;
    }
}