using System.Text;
using System.Text.Json;
using CartingService.Core.Interfaces;
using CartingService.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CartingService.Infrastructure.Notification;

public class NotificationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly RabbitMqConfig _config;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public NotificationService(
        IServiceProvider serviceProvider,
        ConnectionFactory connectionFactory,
        RabbitMqConfig config)
    {
        _serviceProvider = NullGuard.ThrowIfNull(serviceProvider);
        var factory = NullGuard.ThrowIfNull(connectionFactory);
        _config = NullGuard.ThrowIfNull(config);

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _config.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += ConsumerOnReceived;

        _channel.BasicConsume(queue: _config.QueueName,
            autoAck: false,
            consumer: consumer);

        return Task.CompletedTask;
    }

    private void ConsumerOnReceived(object? sender, BasicDeliverEventArgs eventArgs)
    {
        var body = eventArgs.Body.ToArray();
        var rawMsg = Encoding.UTF8.GetString(body);
        var updatePriceMessage = JsonSerializer.Deserialize<UpdateItemPriceMessage>(rawMsg);
        NullGuard.ThrowIfNull(updatePriceMessage);
        
        // hack to call a service using DI container
        using (var scope = _serviceProvider.CreateScope())
        {
            var cartingService = scope.ServiceProvider.GetRequiredService<ICartingService>();
            var itemName = updatePriceMessage!.Name;
            var itemPrice = updatePriceMessage.Price;
            cartingService.UpdateItemPrice(itemName, itemPrice);
        }

        _channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
    }
    
    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}