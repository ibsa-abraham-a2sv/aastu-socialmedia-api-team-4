using Application.Contracts;
using Application.DTOs.Follow;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Follow.Commands.CreateFollow;

public class CreateFollowCommandHandler : IRequestHandler<CreateFollowCommand, FollowDto>
{
    private readonly IFollowRepository _followRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateFollowCommandHandler(IFollowRepository followRepository, IUserRepository userRepository, IMapper mapper)
    {
        _followRepository = followRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<FollowDto> Handle(CreateFollowCommand request, CancellationToken cancellationToken)
    {
        var validator = new FollowDtoValidator(_userRepository);
        var validationResult = await validator.ValidateAsync(request.FollowDto, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var follow = _mapper.Map<FollowEntity>(request.FollowDto);

        follow = await _followRepository.CreateAsync(follow);

        return _mapper.Map<FollowDto>(follow);
    }
}