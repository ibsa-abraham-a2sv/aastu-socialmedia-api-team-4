using Application.DTOs.Notification;
using MediatR;

namespace Application.Features.Notification.Commands.ToggleNotification;

public class ToggleNotificationCommand : IRequest<NotificationDto>
{
    public NotificationDto NotificationDto { get; set; }
}