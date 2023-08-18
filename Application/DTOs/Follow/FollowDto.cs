using Application.DTOs.Common;

namespace Application.DTOs.Follow;

public class FollowDto : BaseDto
{
    public int FollowerId { get; set; }
    public int Following { get; set; }
}