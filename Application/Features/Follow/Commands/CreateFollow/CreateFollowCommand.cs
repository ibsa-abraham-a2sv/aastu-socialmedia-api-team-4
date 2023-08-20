using Application.DTOs.Follow;
using MediatR;

namespace Application.Features.Follow.Commands.CreateFollow;

public class CreateFollowCommand : IRequest<FollowDto>
{
    public FollowDto FollowDto { get; set; }
}