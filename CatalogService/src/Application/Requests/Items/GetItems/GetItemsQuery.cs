using Application.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Requests.Items.GetItems;

public class GetItemsQuery : IRequest<IEnumerable<ItemViewModel>>
{
    public GetItemsModel GetItemsModel { get; }
    
    public GetItemsQuery(GetItemsModel getItemsModel)
    {
        GetItemsModel = NullGuard.ThrowIfNull(getItemsModel);
    }
}

// ReSharper disable once UnusedType.Global
// Used by MediatR
public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IEnumerable<ItemViewModel>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetItemsQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = NullGuard.ThrowIfNull(applicationDbContext);
    }

    public async Task<IEnumerable<ItemViewModel>> Handle(GetItemsQuery request, CancellationToken ct)
    {
        NullGuard.ThrowIfNull(request);
        var model = NullGuard.ThrowIfNull(request.GetItemsModel);

        var allItemsQuery = _applicationDbContext
            .Items
            .Include(i => i.Category)
            .AsNoTracking();

        ApplyCategoryFilter(ref allItemsQuery, model.CategoryName);
        ApplyPagination(ref allItemsQuery, model.PageNum, model.PageSize);

        var result = await allItemsQuery
            .OrderBy(i => i.Name)
            .ToListAsync(ct);

        return result
            .Select(ItemViewModel.FromEntity)
            .ToList();
    }

    private static void ApplyCategoryFilter(ref IQueryable<Item> query, string? categoryName)
    {
        if (!string.IsNullOrWhiteSpace(categoryName))
        {
            query = query.Where(i => i.Category.Name == categoryName);
        }
    }

    private static void ApplyPagination(ref IQueryable<Item> query, int? pageNum, int? pageSize)
    {
        if (pageNum.HasValue && pageSize.HasValue)
        {
            query = query
                .Skip((pageNum.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }
    }
}