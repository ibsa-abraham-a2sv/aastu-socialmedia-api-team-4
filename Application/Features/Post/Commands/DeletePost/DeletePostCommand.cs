
using MediatR;
using Application.DTOs.Post;

namespace Application.Features.Post.Commands.DeletePost;
public class DeletePostCommand : IRequest<Unit>
{
    public int PostID {get; set;}
}

