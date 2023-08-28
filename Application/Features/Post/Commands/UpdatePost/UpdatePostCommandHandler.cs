using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Post.Commands.UpdatePost;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand,Unit>
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    public UpdatePostCommandHandler(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(UpdatePostCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdatePostCommandValidator(){UserRepository = _userRepository};
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    
        var old_post = await _postRepository.GetByIdAsync(command.PostId);
        if (old_post == null)
        {
            throw new NotFoundException($"Post with id {command.PostId} does't exist!", command);
        }

        var post = _mapper.Map<PostEntity>(command.UpdatePost);
        post.Id = command.PostId;
        await _postRepository.UpdateAsync(command.PostId,post);
        return Unit.Value;
    }
}
