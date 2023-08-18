using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Post.Commands.UpdatePost;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand,Unit>
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    public UpdatePostCommandHandler(IPostRepository postRepository, IMapper mapper)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<Unit> Handle(UpdatePostCommand command, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<PostEntity>(command.UpdatePost);
        await _postRepository.UpdateAsync(command.PostID,post);
        return Unit.Value;
    }
}
