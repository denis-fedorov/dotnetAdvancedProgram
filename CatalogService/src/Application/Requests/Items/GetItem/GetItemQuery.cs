using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Requests.Items.GetItem;

public class GetItemQuery : IRequest<ItemViewModel?>
{
    public string Name { get; }
    
    public GetItemQuery(string name)
    {
        Name = NullGuard.ThrowIfNull(name);;
    }
}

// ReSharper disable once UnusedType.Global
// Used by MediatR
public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ItemViewModel?>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetItemQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<ItemViewModel?> Handle(GetItemQuery request, CancellationToken ct)
    {
        var item = await _applicationDbContext
            .Items
            .Where(i => i.Name == request.Name)
            .Include(i => i.Category)
            .AsNoTracking()
            .SingleOrDefaultAsync(ct);

        return item != null
            ? ItemViewModel.FromEntity(item)
            : null;
    }
}