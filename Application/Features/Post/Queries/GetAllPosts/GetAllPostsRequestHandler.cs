
using MediatR;
using AutoMapper;
using Application.DTOs.Post;
using Application.Features.Post.Queries.GetAllPosts;

namespace Application.Features.Post.Queries.GetSinglePost;
public class GetAllPostsRequestHandler : IRequestHandler<GetAllPostsRequest,IReadOnlyList<PostResponseDto>>
{
    public readonly IMapper _mapper;
    public GetAllPostsRequestHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<IReadOnlyList<PostResponseDto>> Handle(GetAllPostsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}