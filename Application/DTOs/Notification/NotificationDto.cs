using Application.DTOs.Common;

namespace Application.DTOs.Notification;

public class NotificationDto : BaseDto
{
    public int UserId { get; set; }
    public string? Content { get; set; }
    public bool ReadStatus { get; set; } = false;
}