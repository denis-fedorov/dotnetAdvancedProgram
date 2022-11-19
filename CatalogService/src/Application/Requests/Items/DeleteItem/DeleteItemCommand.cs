using Application.Exceptions;
using Application.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Requests.Items.DeleteItem;

public class DeleteItemCommand : IRequest
{
    public string Name { get; }

    public DeleteItemCommand(string name)
    {
        Name = NullGuard.ThrowIfNull(name);
    }
}

// ReSharper disable once UnusedType.Global
// Used by MediatR
public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public DeleteItemCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = NullGuard.ThrowIfNull(applicationDbContext);
    }
    
    public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken ct)
    {
        NullGuard.ThrowIfNull(request);

        var name = NullGuard.ThrowIfNull(request.Name);
        var item = await GetItem(name, ct);
        if (item is null)
        {
            throw new ItemNotFoundException(name);
        }

        _applicationDbContext.Items.Remove(item);
        await _applicationDbContext.SaveChangesAsync(ct);
        
        return new Unit();
    }

    private async Task<Item?> GetItem(string name, CancellationToken ct)
    {
        return await _applicationDbContext
            .Items
            .SingleOrDefaultAsync(i => i.Name == name, ct);
    }
} 