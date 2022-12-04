using System.Text;
using System.Text.Json;
using Application.Interfaces;
using Core.Entities;
using RabbitMQ.Client;
using SharedKernel;

namespace Infrastructure.Notification;

public class NotificationService : INotificationService
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly RabbitMqConfig _config;
    
    public NotificationService(ConnectionFactory connectionFactory, RabbitMqConfig config)
    {
        _connectionFactory = NullGuard.ThrowIfNull(connectionFactory);
        _config = NullGuard.ThrowIfNull(config);
    }
    
    public void SendNewPriceMessage(Item item)
    {
        NullGuard.ThrowIfNull(item);
        
        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        var queueName = _config.QueueName;
        channel.QueueDeclare(queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var message = ConvertToMessage(item);
        var body = Encoding.UTF8.GetBytes(message);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        channel.BasicPublish(exchange: "",
            routingKey: queueName,
            basicProperties: properties,
            body: body);
    }
    
    private static string ConvertToMessage(Item item)
    {
        var message = new UpdateItemPriceMessage
        {
            Name = item.Name,
            Price = item.Price
        };

        return JsonSerializer.Serialize(message);
    }
}