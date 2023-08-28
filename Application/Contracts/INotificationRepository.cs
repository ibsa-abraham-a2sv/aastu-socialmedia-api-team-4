using Application.Contracts.Common;
using Application.DTOs.Notification;
using Application.DTOs.User;
using Domain.Entities;

namespace Application.Contracts;

public interface INotificationRepository : IGenericRepository<NotificationEntity>
{
    public Task<NotificationEntity> ToggleNotification (int notificationId);
    public Task<List<NotificationEntity>> GetNotificationsOfUser(int userId);
}