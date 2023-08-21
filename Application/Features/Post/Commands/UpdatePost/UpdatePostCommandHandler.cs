using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
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
        var validator = new UpdatePostCommandValidator();
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var old_comment = await _postRepository.GetByIdAsync(command.PostId);
        if (old_comment != null)
        {
            throw new Exception($"Comment with id {command.PostId} does't exist!");
        }
        var post = _mapper.Map<PostEntity>(command.UpdatePost);
        await _postRepository.UpdateAsync(command.PostId,post);
        return Unit.Value;
    }
}
