
using Domain.Common;

namespace Domain.Entities;

public class PostEntity : BaseDomainEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int UserId { get; set; }
    public UserEntity? User { get; set; }
    public int LikeCount { get; set; }
    public List<LikeEntity>? Likes { get; set; }
    public List<CommentEntity>? Comments { get; set; }
}