using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Categories.GetCategories;

public class GetCategoriesQuery : IRequest<GetCategoriesViewModel>
{ }

// ReSharper disable once UnusedType.Global
// Used by MediatR
public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesViewModel>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetCategoriesQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<GetCategoriesViewModel> Handle(GetCategoriesQuery request, CancellationToken ct)
    {
        var allCategories = await _applicationDbContext
            .Categories
            .Include(c => c.ParentCategory)
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync(ct);

        return GetCategoriesViewModel.FromEntities(allCategories);
    }
}