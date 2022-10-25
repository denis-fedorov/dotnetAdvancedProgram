using Application.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Categories.CreateCategory;

public class CreateCategoryCommand : IRequest
{
    public CreateCategoryModel CreateCategoryModel { get; }
    
    public CreateCategoryCommand(CreateCategoryModel createCategoryModel)
    {
        CreateCategoryModel = createCategoryModel;
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
        
        var model = request.CreateCategoryModel; 
        NullGuard.ThrowIfNull(model);

        var parentCategory = await GetParentCategory(model.ParentCategoryName, ct);

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