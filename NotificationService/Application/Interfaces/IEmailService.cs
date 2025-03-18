using NotificationService.Domain.Models;

namespace NotificationService.Application.Interfaces;

public interface IEmailService
{
    Task Send(EmailSenditems items);
}
