using System.Runtime.Serialization;
using Application.Requests.Categories.GetCategories;
using SharedKernel;

namespace WebApi.Settings.Model;

public sealed class CategoriesResourceList : ResourceBase
{
    [DataMember(Order = 1)]
    public IEnumerable<CategoriesTreeViewModel> Categories { get; private set; }

    public CategoriesResourceList(
        LinkGenerator linkGenerator,
        IHttpContextAccessor httpContextAccessor,
        IEnumerable<CategoriesTreeViewModel> categories)
            :base(linkGenerator, httpContextAccessor)
    {
        Categories = NullGuard.ThrowIfNull(categories);
    }
}