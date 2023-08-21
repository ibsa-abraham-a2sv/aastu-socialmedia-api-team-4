
using MediatR;
using Application.DTOs.Post;

namespace Application.Features.Post.Commands.UpdatePost;
public class UpdatePostCommand : IRequest<Unit>
{
    public int PostId {get; set;}
    public PostRequestDto? UpdatePost {get; set;}
}

