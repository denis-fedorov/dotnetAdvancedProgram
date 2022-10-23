using CartingService.Core.Entities;
using LiteDB;

namespace CartingService.Infrastructure.Settings;

public static class DbMappings
{
    public const string CartsTableName = "carts";
    public const string ItemsTableName = "items";

    public static void UpdateDbMappings(this BsonMapper mapper)
    {
        mapper
            .Entity<Cart>()
            .DbRef(c => c.Items, ItemsTableName);
    }
}