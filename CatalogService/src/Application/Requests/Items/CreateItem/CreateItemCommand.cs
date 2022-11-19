using Application.Exceptions;
using Application.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Requests.Items.CreateItem;

public class CreateItemCommand : IRequest
{
    public CreateItemModel CreateItemModel { get; }
    
    public CreateItemCommand(CreateItemModel createItemModel)
    {
        CreateItemModel = createItemModel;
    }
}

// ReSharper disable once UnusedType.Global
// Used by MediatR
public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CreateItemCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = NullGuard.ThrowIfNull(applicationDbContext);
    }

    public async Task<Unit> Handle(CreateItemCommand request, CancellationToken ct)
    {
        NullGuard.ThrowIfNull(request);
        var model = NullGuard.ThrowIfNull(request.CreateItemModel);
        
        var name = NullGuard.ThrowIfNull(model.Name);
        if (await NameExists(name, ct))
        {
            throw new ItemWithTheSameNameAlreadyExists(name);
        }
        
        var categoryName = NullGuard.ThrowIfNull(model.CategoryName);
        var category = await GetCategory(categoryName, ct);
        if (category is null)
        {
            throw new ItemCategoryNotFoundException(categoryName, model.Name);
        }

        var item = new Item(name, model.Description, model.Image, category, model.Price, model.Amount);
        await _applicationDbContext.Items.AddAsync(item, ct);
        await _applicationDbContext.SaveChangesAsync(ct);

        return new Unit();
    }
    
    private async Task<bool> NameExists(string name, CancellationToken ct)
    {
        return await _applicationDbContext
            .Items
            .Where(i => i.Name == name)
            .AnyAsync(ct);
    }

    private async Task<Category?> GetCategory(string categoryName, CancellationToken ct)
    {
        var category = await _applicationDbContext
            .Categories
            .Where(c => c.Name == categoryName)
            .Include(c => c.ParentCategory)
            .SingleOrDefaultAsync(ct);

        return category;
    }
}