using Application.Exceptions;
using Application.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Requests.Categories.CreateCategory;

public class CreateCategoryCommand : IRequest
{
    public CreateCategoryModel CreateCategoryModel { get; }
    
    public CreateCategoryCommand(CreateCategoryModel createCategoryModel)
    {
        CreateCategoryModel = NullGuard.ThrowIfNull(createCategoryModel);
    }
}

// ReSharper disable once UnusedType.Global
// Used by MediatR
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CreateCategoryCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken ct)
    {
        NullGuard.ThrowIfNull(request);
        var model = NullGuard.ThrowIfNull(request.CreateCategoryModel);
        
        var parentCategoryName = model.ParentCategoryName;
        Category? parentCategory = null;
        if (!string.IsNullOrWhiteSpace(parentCategoryName))
        {
            parentCategory = await GetParentCategory(parentCategoryName, ct);
            if (parentCategory is null)
            {
                throw new ParentCategoryNotFoundException(parentCategoryName);
            }    
        }

        var category = new Category(model.Name, model.Image, parentCategory);
        await _applicationDbContext.Categories.AddAsync(category, ct);
        await _applicationDbContext.SaveChangesAsync(ct);

        return new Unit();
    }

    private async Task<Category?> GetParentCategory(string? parentCategoryName, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(parentCategoryName))
        {
            return null;
        }

        var category = await _applicationDbContext.Categories
            .Where(c => c.Name == parentCategoryName)
            .Include(c => c.ParentCategory)
            .SingleOrDefaultAsync(ct);

        return category;
    }
}