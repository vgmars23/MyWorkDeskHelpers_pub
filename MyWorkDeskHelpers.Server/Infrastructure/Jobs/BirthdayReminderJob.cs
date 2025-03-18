using Quartz;
using MyWorkDeskHelpers.Server.Application.Interfaces;
using MyWorkDeskHelpers.Server.Domain.Entities;
using MyWorkDeskHelpers.Server.Domain.Enums;
using MyWorkDeskHelpers.Server.Domain.Models;

public class BirthdayReminderJob : IJob
{
    private readonly IBirthdayService _birthdayService;
    private readonly IUserContactService _contactService;
    private readonly INotificationPublisher _notificationPublisher;

    public BirthdayReminderJob(IBirthdayService birthdayService, IUserContactService contactService, INotificationPublisher notificationPublisher)
    {
        _birthdayService = birthdayService;
        _contactService = contactService;
        _notificationPublisher = notificationPublisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {

        List<Birthday> allBirthdays = await _birthdayService.GetAllBirthdaysAsync();
        Console.WriteLine("🎯 Запуск джобы для отправки напоминаний о днях рождения!");

        DateTime todayUtc = DateTime.UtcNow.Date;

        foreach (var birthday in allBirthdays)
        {
            DateTime eventDateUtc = DateTimeOffset.FromUnixTimeSeconds(birthday.DateTimestamp).UtcDateTime.Date;

            DateTime reminderDateUtc = eventDateUtc.AddDays(-birthday.ReminderDays);

            if (todayUtc == reminderDateUtc || todayUtc == eventDateUtc)
            {
                Console.WriteLine($"🔔 Уведомление для: {birthday.Name} на {eventDateUtc:yyyy-MM-dd}");
                await _notificationPublisher.PublishNotification(new NotificationMessage
                {
                    Type = NotificationType.Telegram,
                    Recipient = "",
                    Message = birthday.Name
                });
                var contactInfo = await _contactService.GetUserContactByIdAsync(birthday.ContactInfoId);
                if (contactInfo == null)
                {
                    Console.WriteLine($"⚠️ Контактная информация отсутствует для {birthday.Name}");
                    continue;
                }

                string message = $"🎉 Напоминание: {birthday.Name} празднует день рождения {eventDateUtc:yyyy-MM-dd}! 🎂";

                if (!string.IsNullOrEmpty(contactInfo.Email))
                {
                    await _notificationPublisher.PublishNotification(new NotificationMessage
                    {
                        Type = NotificationType.Email,
                        Recipient = "",
                        Message = message
                    });
                }

                if (!string.IsNullOrEmpty(contactInfo.TelegramUsername))
                {
                    await _notificationPublisher.PublishNotification(new NotificationMessage
                    {
                        Type = NotificationType.Telegram,
                        Recipient = "fasfdsaf",
                        Message = message
                    });
                }

            }
        }
    }
}