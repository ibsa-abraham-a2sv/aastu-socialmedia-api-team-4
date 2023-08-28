using Application.Contracts;
using Application.DTOs.Follow;
using Application.Features.Follow.Commands.CreateFollow;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Follow.Commands.DeleteFollow;

public class DeleteFollowCommandHandler : IRequestHandler<DeleteFollowCommand, bool>
{
    private readonly IFollowRepository _followRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public DeleteFollowCommandHandler(IUserRepository userRepository, IMapper mapper, IFollowRepository followRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _followRepository = followRepository;
    }
    
    public async Task<bool> Handle(DeleteFollowCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteFollowCommandValidator(_userRepository, _followRepository);
        var validationResult = await validator.ValidateAsync(request.FollowDto, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var follow = _mapper.Map<FollowEntity>(request.FollowDto);

        await _followRepository.DeleteFollow(follow);

        return true;
    }
}