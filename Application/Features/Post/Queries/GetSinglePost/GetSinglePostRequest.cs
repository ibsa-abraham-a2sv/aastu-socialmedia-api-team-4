
using MediatR;
using Application.DTOs.Post;

namespace Application.Features.Post.Queries.GetSinglePost;
public class GetSinglePostRequest : IRequest<PostResponseDto>
{
    public int Id { get; set; }
}