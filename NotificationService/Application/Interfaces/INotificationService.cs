using NotificationService.Domain.Enums;

namespace NotificationService.Application.Interfaces;

public interface INotificationService
{
    Task SendNotificationAsync(NotificationType type, string recipient, string message);
}
