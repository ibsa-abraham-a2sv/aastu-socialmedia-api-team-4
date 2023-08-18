
using MediatR;
using Application.DTOs.Post;

namespace Application.Features.Post.Commands.CreatePost;
public class CreatePostCommand : IRequest<Unit>
{
    public PostRequestDto? NewPost {get; set;}
}

