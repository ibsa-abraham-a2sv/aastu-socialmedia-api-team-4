using Application.DTOs.Common;

namespace Application.DTOs.Like;

public class LikeDto : BaseDto
{
    public int UserId { get; set; }
    public int PostId { get; set; }
}