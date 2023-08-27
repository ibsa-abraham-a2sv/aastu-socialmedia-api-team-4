
using MediatR;
using Application.DTOs.Post;
using Domain.Entities;

namespace Application.Features.Post.Commands.CreatePost;
public class CreatePostCommand : IRequest<PostResponseDto>
{
    public PostRequestDto? NewPost {get; set;}
}

