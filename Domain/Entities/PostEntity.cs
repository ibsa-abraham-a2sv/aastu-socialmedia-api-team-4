
using Domain.Common;

namespace Domain.Entities;

public class PostEntity : BaseDomainEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string PicturePath { get; set; } = string.Empty;
    public UserEntity? User { get; set; }
    public List<LikeEntity>? Likes { get; set; }
    public List<CommentEntity>? Comments { get; set; }
    public int LikeCount { get; set; } = 0;
    public List<TagEntity> Tags { get; set; } = null!;
}