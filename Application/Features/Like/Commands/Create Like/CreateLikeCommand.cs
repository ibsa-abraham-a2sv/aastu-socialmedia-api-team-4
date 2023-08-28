using Application.DTOs.Like;
using MediatR;

namespace Application.Features.Like.Commands.Create_Like;

public class CreateLikeCommand : IRequest<LikeDto>
{
    public LikeDto LikeDto { get; set; } = null!;
}