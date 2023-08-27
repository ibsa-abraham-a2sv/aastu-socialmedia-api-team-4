using MediatR;
using AutoMapper;
using Application.DTOs.Post;
using Application.Features.Post.Queries.GetAllPostsByUserId;
using Application.Exceptions;
using Application.Contracts;

namespace Application.Features.Post.Queries.GetAllPostsByUserId;
public class GetAllPostsByUserIdRequestHandler : IRequestHandler<GetAllPostsByUserIdRequest,IReadOnlyList<PostResponseDto>>
{
    public readonly IMapper _mapper;
    public readonly IPostRepository _postRepository;
    public readonly IUserRepository _userRepository;
    public GetAllPostsByUserIdRequestHandler(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<IReadOnlyList<PostResponseDto>> Handle(GetAllPostsByUserIdRequest request, CancellationToken cancellationToken)
    {
        bool res = await _userRepository.Exists(request.UserId);
        if(res == false)
             throw new NotFoundException($"Post with id {request.UserId} does't exist!", request);

        var post =  await _postRepository.GetAllPostsByUserIdAsync(request.UserId);
        return _mapper.Map<IReadOnlyList<PostResponseDto>>(post);
    }
}