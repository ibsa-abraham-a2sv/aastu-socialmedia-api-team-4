using Application.Contracts;
using Application.DTOs.Notification;
using Application.DTOs.User;
using Application.Exceptions;
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

    public async Task<NotificationEntity> ToggleNotification(int notificationId)
    {
        var notification = _dbContext.Notification.FirstOrDefault(n => n.Id == notificationId);

        if (notification == null)
            throw new NotFoundException($"Notification not found!", notification);

        notification.ReadStatus = !notification.ReadStatus;

        await _dbContext.SaveChangesAsync();

        return notification;
    }

    public async Task<List<NotificationEntity>> GetNotificationsOfUser(int userId)
    {
        var notifications = await _dbContext.Notification
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

        return notifications;
    }
}