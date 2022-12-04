using Application.Exceptions;
using Application.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Requests.Items.UpdateItem;

public sealed class UpdateItemCommand : IRequest
{
    public string ItemName { get; }
    public UpdateItemModel UpdateItemModel { get; }

    public UpdateItemCommand(string itemName, UpdateItemModel updateItemModel)
    {
        ItemName = NullGuard.ThrowIfNull(itemName);
        UpdateItemModel = NullGuard.ThrowIfNull(updateItemModel);
    }
}

// ReSharper disable once UnusedType.Global
// Used by MediatR
public sealed class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly INotificationService _notificationService;

    public UpdateItemCommandHandler(
        IApplicationDbContext applicationDbContext,
        INotificationService notificationService)
    {
        _applicationDbContext = applicationDbContext;
        _notificationService = notificationService;
    }

    public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        NullGuard.ThrowIfNull(request);
        var model = NullGuard.ThrowIfNull(request.UpdateItemModel);
        var name = NullGuard.ThrowIfNull(request.ItemName);

        var item = await GetItem(name, cancellationToken);
        if (item is null)
        {
            throw new ItemNotFoundException(name);
        }

        if (item.TryUpdatePrice(model.Price))
        {
            _applicationDbContext.Items.Update(item);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            
            _notificationService.SendNewPriceMessage(item);
        }
        
        return new Unit();
    }
    
    private async Task<Item?> GetItem(string name, CancellationToken ct)
    {
        return await _applicationDbContext
            .Items
            .SingleOrDefaultAsync(i => i.Name == name, ct);
    }
}
