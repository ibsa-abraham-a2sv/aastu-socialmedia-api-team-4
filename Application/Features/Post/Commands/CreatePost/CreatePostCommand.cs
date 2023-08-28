
using MediatR;
using Application.DTOs.Post;
using Domain.Entities;

namespace Application.Features.Post.Commands.CreatePost;
public class CreatePostCommand : IRequest<PostResponseDto>
{
    public int UserId { get; set; }
    public PostRequestDto NewPost {get; set;} = null!;
}

