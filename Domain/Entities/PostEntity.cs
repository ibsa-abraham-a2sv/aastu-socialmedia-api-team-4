
using Domain.Common;

namespace Domain.Entites;

public class PostEntity : BaseDomainEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int LikeCount { get; set; }
}