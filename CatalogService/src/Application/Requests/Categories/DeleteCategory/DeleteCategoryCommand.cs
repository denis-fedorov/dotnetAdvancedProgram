using Application.Exceptions;
using Application.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Requests.Categories.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public string Name { get; }

    public DeleteCategoryCommand(string name)
    {
        Name = NullGuard.ThrowIfNull(name);
    }
}

// ReSharper disable once UnusedType.Global
// Used by MediatR
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public DeleteCategoryCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = NullGuard.ThrowIfNull(applicationDbContext);
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken ct)
    {
        NullGuard.ThrowIfNull(request);

        var name = NullGuard.ThrowIfNull(request.Name);
        var category = await GetCategory(name, ct);
        if (category is null)
        {
            throw new CategoryNotFoundException(name);
        }

        if (await NestedCategoriesExist(name, ct))
        {
            throw new RootCategoryDeleteException(name);
        }

        var items = await GetItems(name, ct);
        
        _applicationDbContext.Categories.Remove(category);
        if (items.Count != 0)
        {
            _applicationDbContext.Items.RemoveRange(items);    
        }

        await _applicationDbContext.SaveChangesAsync(ct);
        
        return new Unit();
    }
    
    private async Task<Category?> GetCategory(string name, CancellationToken ct)
    {
        return await _applicationDbContext
            .Categories
            .SingleOrDefaultAsync(i => i.Name == name, ct);
    }

    private async Task<bool> NestedCategoriesExist(string name, CancellationToken ct)
    {
        return await _applicationDbContext
            .Categories
            .AnyAsync(category =>
                category.ParentCategory != null &&
                category.ParentCategory.Name == name, ct);
    }

    private async Task<List<Item>> GetItems(string categoryName, CancellationToken ct)
    {
        return await _applicationDbContext
            .Items
            .Where(i => i.Category.Name == categoryName)
            .ToListAsync(ct);
    }
}