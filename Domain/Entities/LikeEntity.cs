using Domain.Common;

namespace Domain.Entities;

public class LikeEntity : BaseDomainEntity
{
    public int UserId { get; set; }
    public int PostId { get; set; }
}