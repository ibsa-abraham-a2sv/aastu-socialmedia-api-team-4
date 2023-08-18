using Domain.Common;

namespace Domain.Entities;

public class FollowEntity : BaseDomainEntity
{
    public int FollowerId { get; set; }
    public int Following { get; set; }
}