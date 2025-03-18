using MyWorkDeskHelpers.Server.Domain.Models;

namespace MyWorkDeskHelpers.Server.Application.Interfaces
{
    public interface INotificationPublisher
    {
        Task PublishNotification(NotificationMessage notification);
    }


}
