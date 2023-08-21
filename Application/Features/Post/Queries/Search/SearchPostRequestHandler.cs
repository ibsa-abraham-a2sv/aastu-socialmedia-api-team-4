
using MediatR;
using AutoMapper;
using Application.DTOs.Post;

namespace Application.Features.Post.Queries.SearchPost;
public class SearchPostRequestHandler : IRequestHandler<SearchPostRequest,IReadOnlyList<PostResponseDto>>
{
    public readonly IMapper _mapper;
    public readonly IPostRepository _postRepository;
    public SearchPostRequestHandler(IPostRepository postRepository, IMapper mapper)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<IReadOnlyList<PostResponseDto>> Handle(SearchPostRequest request, CancellationToken cancellationToken)
    {
        var posts =  await _postRepository.SearchPostsAsync(request.Query);
        return _mapper.Map<IReadOnlyList<PostResponseDto>>(posts);   
    }
}