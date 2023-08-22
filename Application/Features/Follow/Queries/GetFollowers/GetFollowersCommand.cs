using Application.DTOs.User;
using MediatR;

namespace Application.Features.Follow.Queries.GetFollowers;

public class GetFollowersCommand : IRequest<List<UserResponseDto>>
{
    public int UserId { get; set; }
}