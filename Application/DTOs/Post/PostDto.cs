
using Application.DTOs.Common;

namespace Application.DTOs.Post;

public class PostDto : BaseDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int LikeCount { get; set; }
}