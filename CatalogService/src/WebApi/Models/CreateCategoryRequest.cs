namespace WebApi.Models;

public sealed class CreateCategoryRequest
{
    public string Name { get; set; }

    public string? Image { get; set; }

    public string? ParentCategoryName { get; set; }
}