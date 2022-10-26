using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Categories.GetCategory;

public class GetCategoryQuery : IRequest<CategoryViewModel>
{
    public string Name { get; }

    public GetCategoryQuery(string name)
    {
        Name = name;
    }
}

// ReSharper disable once UnusedType.Global
// Used by MediatR
public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryViewModel?>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetCategoryQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<CategoryViewModel?> Handle(GetCategoryQuery request, CancellationToken ct)
    {
        var category = await _applicationDbContext
            .Categories
            .Where(c => c.Name == request.Name)
            .Include(c => c.ParentCategory)
            .AsNoTracking()
            .SingleOrDefaultAsync(ct);

        return category != null
            ? CategoryViewModel.FromEntity(category)
            : null;
    }
}