using Application.DTOs.Notification;
using Domain.Entities;
using MediatR;

public class CreateNotificationCommand : IRequest<NotificationDto>
{
    public NotificationDto NotificationDto { get; set; } = null!;
}