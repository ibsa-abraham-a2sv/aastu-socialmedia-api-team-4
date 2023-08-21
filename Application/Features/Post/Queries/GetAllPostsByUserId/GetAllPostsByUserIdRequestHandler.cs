
using MediatR;
using AutoMapper;
using Application.DTOs.Post;
using Application.Features.Post.Queries.GetAllPostsByUserId;

namespace Application.Features.Post.Queries.GetAllPostsByUserId;
public class GetAllPostsByUserIdRequestHandler : IRequestHandler<GetAllPostsByUserIdRequest,IReadOnlyList<PostResponseDto>>
{
    public readonly IMapper _mapper;
    public readonly IPostRepository _postRepository;
    public GetAllPostsByUserIdRequestHandler(IPostRepository postRepository, IMapper mapper)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<IReadOnlyList<PostResponseDto>> Handle(GetAllPostsByUserIdRequest request, CancellationToken cancellationToken)
    {
        var post =  await _postRepository.GetAllPostsByUserIdAsync(request.UserId);
        return _mapper.Map<IReadOnlyList<PostResponseDto>>(post);
    }
}