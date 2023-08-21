using Domain.Common;

namespace Domain.Entities;

public class LikeEntity : BaseDomainEntity
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public UserEntity? User { get; set; }
    public PostEntity? Post { get; set; }    
}