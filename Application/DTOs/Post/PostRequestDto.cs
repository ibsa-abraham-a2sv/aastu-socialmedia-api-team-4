
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Post;

public class PostRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public IFormFile? PictureFile { get; set; } = null;
} 