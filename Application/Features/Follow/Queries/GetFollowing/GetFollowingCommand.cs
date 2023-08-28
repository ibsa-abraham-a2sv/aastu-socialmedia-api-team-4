using Application.DTOs.User;
using MediatR;

namespace Application.Features.Follow.Queries.GetFollowing;

public class GetFollowingCommand : IRequest<List<UserResponseDto>>
{
    public int UserId { get; set; }
}