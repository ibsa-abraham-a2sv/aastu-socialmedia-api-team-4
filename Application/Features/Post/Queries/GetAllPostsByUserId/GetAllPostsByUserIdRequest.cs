using MediatR;
using Application.DTOs.Post;

namespace Application.Features.Post.Queries.GetAllPostsByUserId;
public class GetAllPostsByUserIdRequest : IRequest<IReadOnlyList<PostResponseDto>>
{
    public int UserId { get; set; }
}