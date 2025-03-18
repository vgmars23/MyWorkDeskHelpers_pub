namespace NotificationService.Domain.Models
{
    public class TelegramSendItems
    {
        public string Message { get; set; } = string.Empty;
        public int ChatId { get; set; }
    }
}
