using Application.DTOs.Common;

namespace Application.DTOs.Follow;

public class FollowDto : BaseDto
{
    public int FollowerId { get; set; }
    public int FollowingId { get; set; }
}