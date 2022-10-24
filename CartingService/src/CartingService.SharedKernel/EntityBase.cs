namespace CartingService.SharedKernel;

public abstract class EntityBase
{
    public string Id { get; protected init; } = string.Empty;
}