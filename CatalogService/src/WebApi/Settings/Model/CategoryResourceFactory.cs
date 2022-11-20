using Application.Requests.Categories.GetCategories;
using SharedKernel;
using WebApi.Controllers;

namespace WebApi.Settings.Model;

public sealed class CategoryResourceFactory
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string GetCategoryRouteName = nameof(CategoriesController.Get);
    private const string CreateCategoryRouteName = nameof(CategoriesController.Create);
    private const string DeleteCategoryRouteName = nameof(CategoriesController.Delete);
    
    public CategoryResourceFactory(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
    {
        _linkGenerator = NullGuard.ThrowIfNull(linkGenerator);
        _httpContextAccessor = NullGuard.ThrowIfNull(httpContextAccessor);
    }

    public ResourceBase CreateCategoriesResourceList(IEnumerable<CategoriesTreeViewModel> categories)
    {
        return new CategoriesResourceList(_linkGenerator, _httpContextAccessor ,categories)
            .AddGet("category", GetCategoryRouteName, new { name = "<categoryName>"})
            .AddPost("create-category", CreateCategoryRouteName)
            .AddDelete("delete-category", DeleteCategoryRouteName, new { name = "<categoryName>"});
    }
}