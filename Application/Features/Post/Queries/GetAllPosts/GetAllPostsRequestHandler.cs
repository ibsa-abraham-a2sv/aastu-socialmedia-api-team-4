
using MediatR;
using AutoMapper;
using Application.DTOs.Post;
using Application.Features.Post.Queries.GetAllPosts;

namespace Application.Features.Post.Queries.GetSinglePost;
public class GetAllPostsRequestHandler : IRequestHandler<GetAllPostsRequest,IReadOnlyList<PostResponseDto>>
{
    public readonly IMapper _mapper;
    public readonly IPostRepository _postRepository;
    public GetAllPostsRequestHandler(IPostRepository postRepository, IMapper mapper)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<IReadOnlyList<PostResponseDto>> Handle(GetAllPostsRequest request, CancellationToken cancellationToken)
    {
        var posts =  await _postRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<PostResponseDto>>(posts);
    }
}