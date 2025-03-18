using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Models;

namespace NotificationService.Application.Services;

public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _senderEmail;
    private readonly string _senderPassword;

    public EmailService(IConfiguration configuration)
    {
        var smtpSettings = configuration.GetSection("SmtpSettings");

        _smtpServer = smtpSettings["Server"]!;
        _smtpPort = int.Parse(smtpSettings["Port"]!);
        _senderEmail = smtpSettings["SenderEmail"]!;
        _senderPassword = smtpSettings["SenderPassword"]!;
    }

    public async Task Send(EmailSenditems items)
    {
        if (items.Recipient == null)
        {
            Console.WriteLine($"⚠️ Контактная информация отсутствует");
            return;
        }

        using var smtpClient = new SmtpClient(_smtpServer)
        {
            Port = _smtpPort,
            Credentials = new NetworkCredential(_senderEmail, _senderPassword),
            EnableSsl = true
        };

        using var mailMessage = new MailMessage(_senderEmail, items.Recipient)
        {
            Subject = "📩 Уведомление",
            Body = items.Message,
            IsBodyHtml = false
        };

        await smtpClient.SendMailAsync(mailMessage);
    }
}
