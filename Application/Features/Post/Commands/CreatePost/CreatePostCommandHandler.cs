using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Post.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand,Unit>
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    public CreatePostCommandHandler(IPostRepository postRepository, IMapper mapper)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<Unit> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<PostEntity>(command.NewPost);
        await _postRepository.CreateAsync(post);

        return Unit.Value;
    }
}
