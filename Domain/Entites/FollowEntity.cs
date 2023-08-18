using Domain.Common;

namespace Domain.Entites;

public class FollowEntity : BaseDomainEntity
{
    public int FollowerId { get; set; }
    public int Following { get; set; }
}