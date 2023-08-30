
using Application.DTOs.Comment;
using Application.DTOs.Common;
using Application.DTOs.Like;
using Application.DTOs.User;

namespace Application.DTOs.Post;

public class PostResponseDto : BaseDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string PicturePath { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int LikeCount { get; set; }
    public List<LikeDto>? Likes{ get; set; }
    public List<CommentResponseDTO>? Comments { get; set; }
}