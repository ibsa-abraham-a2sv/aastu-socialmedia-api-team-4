using Domain.Common;

namespace Domain.Entites;

public class LikeEntity : BaseDomainEntity
{
    public int UserId { get; set; }
    public int PostId { get; set; }
}