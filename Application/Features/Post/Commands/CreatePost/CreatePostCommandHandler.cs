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

namespace Application.Features.Post.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand,Unit>
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    
    public CreatePostCommandHandler(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreatePostCommandValidator(){UserRepository = _userRepository};
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var post = _mapper.Map<PostEntity>(command.NewPost);
        await _postRepository.CreateAsync(post);

        return Unit.Value;
    }
}
