using NotificationService.Domain.Models;

namespace NotificationService.Application.Interfaces
{
    public interface INotificationHandler
    {
        Task HandleNotification(NotificationMessage notification);
    }

}
