using Domain.Common;

namespace Domain.Entities;

public class FollowEntity : BaseDomainEntity
{
    public int FollowerId { get; set; }
    public int FollowingId { get; set; }
    public UserEntity? Follower { get; set; }
    public UserEntity? Following { get; set; }
}