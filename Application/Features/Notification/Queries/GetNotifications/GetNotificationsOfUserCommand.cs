using Application.DTOs.Notification;
using Application.DTOs.User;
using MediatR;

namespace Application.Features.Notification.Queries.GetNotifications;

public class GetNotificationsOfUserCommand : IRequest<List<NotificationDto>>
{
    public int UserId { get; set; }
}