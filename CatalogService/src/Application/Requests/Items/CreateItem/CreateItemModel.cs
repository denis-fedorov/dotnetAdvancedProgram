namespace Application.Requests.Items.CreateItem;

public sealed class CreateItemModel
{
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Image { get; set; }
    
    public string CategoryName { get; set; }
    
    public decimal Price { get; set; }
    
    public uint Amount { get; set; }
}