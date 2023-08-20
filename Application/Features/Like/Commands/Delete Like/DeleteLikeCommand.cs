using MediatR;

namespace Application.Features.Like.Commands.Delete_Like;

public class DeleteLikeCommand : IRequest<bool>
{
    public int Id { get; set; }
}