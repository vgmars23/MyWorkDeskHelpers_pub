using NotificationService.Application.Interfaces;
using NotificationService.Domain.Enums;
using NotificationService.Domain.Models;

public class NotificationHandler : INotificationHandler
{
    private readonly IEmailService _emailService;
    private readonly ITelegramService _telegramService;

    public NotificationHandler(IEmailService emailService, ITelegramService telegramService)
    {
        _emailService = emailService;
        _telegramService = telegramService;
    }

    public async Task HandleNotification(NotificationMessage notification)
    {
        switch (notification.Type)
        {
            case NotificationType.Email:
                await _emailService.Send(new EmailSenditems() { Message=notification.Message, Recipient=notification.Recipient});
                break;

            case NotificationType.Telegram:
                await _telegramService.Send(new TelegramSendItems() { Message = notification.Message, ChatId = Convert.ToInt32(notification.Recipient)});
                break;

            default:
                Console.WriteLine("❌ Неизвестный тип уведомления.");
                break;
        }
    }
}
