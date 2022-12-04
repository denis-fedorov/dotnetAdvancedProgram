namespace CartingService.Infrastructure.Notification;

public sealed class RabbitMqConfig
{
    public string Host { get; set; }

    public string QueueName { get; set; }
}