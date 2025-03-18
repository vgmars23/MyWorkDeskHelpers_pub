﻿using NotificationService.Domain.Enums;

namespace NotificationService.Application.DTOs;

public class NotificationRequest
{
    public NotificationType Type { get; set; } 
    public string Recipient { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
