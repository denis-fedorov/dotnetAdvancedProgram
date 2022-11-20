using Core.Entities;

namespace Application.Requests.Items.GetItems;

public sealed class GetItemsModel
{
    public string? CategoryName { get; set; }

    public int? PageSize { get; set; }

    public int? PageNum { get; set; }

    public bool IsValid()
    {
        if ((CategoryName?.Length ?? 0) > Category.NameMaxLength)
        {
            return false;
        }

        var onlyPageSizeHasValue = PageSize is null && PageNum.HasValue;
        var onlyPageNumHasValue = PageSize.HasValue && PageNum is null;
        if (onlyPageNumHasValue || onlyPageSizeHasValue)
        {
            return false;
        }

        if (PageSize is < 1 or > 100)
        {
            return false;
        }
        
        if (PageNum is < 1 or > 100)
        {
            return false;
        }

        return true;
    }
}