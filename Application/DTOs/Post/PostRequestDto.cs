
namespace Application.DTOs.Post;

public class PostRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int UserId { get; set; }
}