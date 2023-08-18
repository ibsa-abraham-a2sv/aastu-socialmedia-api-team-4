using MediatR;
using Application.DTOs.Post;

namespace Application.Features.Post.Queries.GetAllPosts;
public class GetAllPostsRequest : IRequest<IReadOnlyList<PostResponseDto>>
{

}