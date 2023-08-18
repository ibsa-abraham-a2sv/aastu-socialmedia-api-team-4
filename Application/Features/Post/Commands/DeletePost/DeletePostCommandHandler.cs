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
    public DeletePostCommandHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
