using Microsoft.Extensions.Configuration;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

public class NotificationConsumer : INotificationConsumer
{
    private readonly INotificationHandler _notificationHandler;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;

    public NotificationConsumer(INotificationHandler notificationHandler, IConfiguration configuration)
    {
        _notificationHandler = notificationHandler;

        var rabbitMqSettings = configuration.GetSection("RabbitMQ");
        var factory = new ConnectionFactory()
        {
            HostName = rabbitMqSettings["Host"]!,
            Port = int.Parse(rabbitMqSettings["Port"]!),
            UserName = rabbitMqSettings["Username"]!,
            Password = rabbitMqSettings["Password"]!
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _queueName = rabbitMqSettings["QueueName"]!;

        _channel.QueueDeclare(queue: _queueName,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        Task.Run(() => StartListening());
    }

    public void StartListening()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");

            var notification = JsonSerializer.Deserialize<NotificationMessage>(message);
            if (notification != null)
            {
                await _notificationHandler.HandleNotification(notification);
            }
        };

        _channel.BasicConsume(queue: _queueName,
                              autoAck: true,
                              consumer: consumer);

        Console.WriteLine(" [*] Waiting for messages.");
    }
}
