using Application.DTOs.Follow;
using MediatR;

namespace Application.Features.Follow.Commands.DeleteFollow;

public class DeleteFollowCommand : IRequest<bool>
{
    public FollowDto FollowDto { get; set; } = null!;
}