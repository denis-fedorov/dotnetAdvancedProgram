using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.GetCategories;

public class GetCategoriesQuery : IRequest<IEnumerable<CategoryViewModel>>
{ }

// ReSharper disable once UnusedType.Global
// Used by MediatR
public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryViewModel>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetCategoriesQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<CategoryViewModel>> Handle(GetCategoriesQuery request, CancellationToken ct)
    {
        var allCategories = await _applicationDbContext
            .Categories
            .Include(c => c.ParentCategory)
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync(ct);

        return allCategories
            .Select(CategoryViewModel.FromEntity)
            .ToList();
    }
}