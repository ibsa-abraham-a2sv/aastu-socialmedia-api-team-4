using Application.DTOs.Common;
using Application.DTOs.Post;

namespace Application.DTOs.Tag;

public class TagResponseDto : BaseDto
{
    public string Name { get; set; } = null!;
    public List<PostResponseDto> Posts { get; set; } = null!;
}