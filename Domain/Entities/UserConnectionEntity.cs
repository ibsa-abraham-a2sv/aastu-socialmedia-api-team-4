using Domain.Common;

namespace Domain.Entities;

public class UserConnectionEntity : BaseDomainEntity
{
    public int UserId { get; set; }
    public string? ConnectionId { get; set; }
    public UserEntity? UserEntity { get; set; }
}