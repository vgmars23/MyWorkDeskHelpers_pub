using System.Text;
using Microsoft.Extensions.Configuration;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Models;

namespace NotificationService.Infrastructure.Telegram;

public class TelegramService : ITelegramService
{
    private readonly string _botToken;

    public TelegramService(IConfiguration configuration)
    {
        _botToken = configuration["Telegram:BotToken"]!;
    }

    public async Task Send(TelegramSendItems items)
    {
        try
        {
            if (items.ChatId == 0)
            {
                Console.WriteLine($"⚠️ Контактная информация отсутствует");
                return;
            }

            string url = $"https://api.telegram.org/bot{_botToken}/sendMessage";
            var json = $"{{\"chat_id\":\"{items.ChatId}\",\"text\":\"{items.Message}\"}}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, content);
            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);
        }
        catch (Exception e)
        {
            Console.WriteLine($"❌ Ошибка отправки в Telegram: {e.Message}");
        }
    }
}
