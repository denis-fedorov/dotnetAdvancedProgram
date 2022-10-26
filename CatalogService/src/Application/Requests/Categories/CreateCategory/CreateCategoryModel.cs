namespace Application.Requests.Categories.CreateCategory;

public sealed class CreateCategoryModel
{
    public string Name { get; set; }

    public string? Image { get; set; }

    public string? ParentCategoryName { get; set; }
}