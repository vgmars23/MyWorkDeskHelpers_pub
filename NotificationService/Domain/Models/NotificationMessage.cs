using NotificationService.Domain.Enums;


namespace NotificationService.Domain.Models
{
    public class NotificationMessage
    {
        public NotificationType Type { get; set; }
        public string Recipient { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
