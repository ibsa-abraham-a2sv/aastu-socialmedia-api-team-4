using Application.Contracts;
using Application.DTOs.Notification;
using Application.DTOs.User;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class NotificationRepository : GenericRepository<NotificationEntity>, INotificationRepository
{
    private readonly AppDBContext _dbContext;

    public NotificationRepository(AppDBContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<NotificationEntity> ToggleNotification(NotificationEntity notificationDto)
    {
        var notification = _dbContext.Notification.FirstOrDefault(notificationDto);

        notification.ReadStatus = !notification.ReadStatus;

        await _dbContext.SaveChangesAsync();

        return notification;
    }

    public async Task<List<NotificationEntity>> GetNotificationsOfUser(int userId)
    {
        var notifications = await _dbContext.Notification
            .Where(n => n.UserId == userId)
            .ToListAsync();

        return notifications;
    }
}