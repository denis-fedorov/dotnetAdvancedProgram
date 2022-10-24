namespace CartingService.Infrastructure.Settings;

public sealed class ConnectionSettings
{
    public const string ConnectionName = "LiteDb";
    public string? ConnectionString { get; set; }
}