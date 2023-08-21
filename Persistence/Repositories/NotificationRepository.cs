using Domain.Entities;

namespace Persistence.Repositories;

public class NotificationRepository : GenericRepository<NotificationEntity>
{
    public NotificationRepository(AppDBContext dbContext) : base(dbContext)
    {
    }
}