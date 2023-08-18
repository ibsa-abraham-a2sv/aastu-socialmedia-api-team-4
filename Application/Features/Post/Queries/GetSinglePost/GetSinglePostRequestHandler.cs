
using MediatR;
using AutoMapper;
using Application.DTOs.Post;

namespace Application.Features.Post.Queries.GetSinglePost;
public class GetSinglePostRequestHandler : IRequestHandler<GetSinglePostRequest,PostResponseDto>
{
    public readonly IMapper _mapper;
    public readonly IPostRepository _postRepository;
    public GetSinglePostRequestHandler(IPostRepository postRepository, IMapper mapper)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<PostResponseDto> Handle(GetSinglePostRequest request, CancellationToken cancellationToken)
    {
        var post =  await _postRepository.GetByIdAsync(request.PostId);
        return _mapper.Map<PostResponseDto>(post);
    }
}