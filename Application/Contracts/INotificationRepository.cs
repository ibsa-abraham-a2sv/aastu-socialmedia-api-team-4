using Application.Contracts.Common;
using Application.DTOs.Notification;
using Application.DTOs.User;
using Domain.Entities;

namespace Application.Contracts;

public interface INotificationRepository : IGenericRepository<NotificationEntity>
{
    public Task<NotificationDto> ToggleNotification (NotificationEntity notificationDto);
    public Task<List<NotificationDto>> GetNotificationsOfUser(UserRequestDto userRequestDto);
}