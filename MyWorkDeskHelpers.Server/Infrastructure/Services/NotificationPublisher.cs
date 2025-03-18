using MyWorkDeskHelpers.Server.Application.Interfaces;
using MyWorkDeskHelpers.Server.Domain.Models;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

public class NotificationPublisher : INotificationPublisher
{
    public async Task PublishNotification(NotificationMessage notification)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "notifications",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        string message = JsonSerializer.Serialize(notification);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                             routingKey: "notifications",
                             basicProperties: null,
                             body: body);

        Console.WriteLine($" [x] Sent {message}");

        await Task.CompletedTask; 
    }
}
