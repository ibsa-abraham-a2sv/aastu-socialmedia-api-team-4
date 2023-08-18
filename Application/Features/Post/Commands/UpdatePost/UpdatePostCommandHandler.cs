using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace Application.Features.Post.Commands.UpdatePost;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand,Unit>
{
    private readonly IMapper _mapper;
    public UpdatePostCommandHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
