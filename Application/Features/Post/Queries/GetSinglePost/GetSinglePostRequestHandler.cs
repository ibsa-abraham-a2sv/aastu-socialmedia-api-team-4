
using MediatR;
using AutoMapper;
using Application.DTOs.Post;

namespace Application.Features.Post.Queries.GetSinglePost;
public class GetSinglePostRequestHandler : IRequestHandler<GetSinglePostRequest,PostResponseDto>
{
    public readonly IMapper _mapper;
    public GetSinglePostRequestHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<PostResponseDto> Handle(GetSinglePostRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}