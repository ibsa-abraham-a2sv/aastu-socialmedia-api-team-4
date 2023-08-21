using MediatR;
using Application.DTOs.Post;

namespace Application.Features.Post.Queries.SearchPost;
public class SearchPostRequest : IRequest<IReadOnlyList<PostResponseDto>>
{
    public string Query { get; set; } = string.Empty;
}