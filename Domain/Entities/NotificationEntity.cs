using Domain.Common;

namespace Domain.Entities;

public class NotificationEntity : BaseDomainEntity
{
    public int UserId { get; set; }
    public string? Content { get; set; }
    public bool ReadStatus { get; set; }
}