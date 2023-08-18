using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace Application.Features.Post.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand,Unit>
{
    private readonly IMapper _mapper;
    public CreatePostCommandHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
