using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Requests.Items.GetItems;

public class GetItemsQuery : IRequest<IEnumerable<ItemViewModel>>
{ }

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
        var allItems = await _applicationDbContext
            .Items
            .Include(i => i.Category)
            .AsNoTracking()
            .OrderBy(i => i.Name)
            .ToListAsync(ct);

        return allItems
            .Select(ItemViewModel.FromEntity)
            .ToList();
    }
}