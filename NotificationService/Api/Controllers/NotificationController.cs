using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Models;

[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly INotificationHandler _notificationHandler;

    public NotificationController(INotificationHandler notificationHandler)
    {
        _notificationHandler = notificationHandler;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendNotification(NotificationMessage notification)
    {
        if (notification == null)
        {
            return BadRequest("Уведомление не может быть пустым.");
        }

        await _notificationHandler.HandleNotification(notification);

        return Ok(new { message = "Уведомление успешно отправлено." });
    }
}
