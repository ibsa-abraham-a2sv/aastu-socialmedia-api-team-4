using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace Application.Features.Post.Commands.DeletePost;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand,Unit>
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    public DeletePostCommandHandler(IPostRepository postRepository, IMapper mapper)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<Unit> Handle(DeletePostCommand command, CancellationToken cancellationToken)
    {
        var post =  await _postRepository.DeleteAsync(command.PostID);
        return Unit.Value;
    }
}
