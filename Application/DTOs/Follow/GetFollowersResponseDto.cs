using Application.DTOs.User;

namespace Application.DTOs.Follow;

public class GetFollowersResponseDto
{
    public List<UserResponseDto>? Followers { get; set; }
}
