using NotificationService.Domain.Models;

namespace NotificationService.Application.Interfaces;

public interface ITelegramService
{
    Task Send(TelegramSendItems items);
}
