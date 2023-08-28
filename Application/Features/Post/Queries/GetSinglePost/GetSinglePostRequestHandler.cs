
using MediatR;
using AutoMapper;
using Application.DTOs.Post;
using Application.Exceptions;

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
        bool res = await _postRepository.Exists(request.PostId);
        if(res == false)
             throw new NotFoundException($"Post with id {request.PostId} does't exist!", request);

        var post =  await _postRepository.GetByIdAsync(request.PostId);
        return _mapper.Map<PostResponseDto>(post);
    }
}