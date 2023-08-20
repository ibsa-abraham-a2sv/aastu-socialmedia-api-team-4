using Application.Contracts;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Follow.Queries.GetFollowers;

public class GetFollowersCommandHandler : IRequestHandler<GetFollowersCommand, List<UserResponseDto>>
{
    private readonly IFollowRepository _followRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetFollowersCommandHandler(IFollowRepository followRepository, IUserRepository userRepository, IMapper mapper)
    {
        _followRepository = followRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<List<UserResponseDto>> Handle(GetFollowersCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetFollowersCommandValidator(_userRepository);
        var validationResult = validator.Validate(request.UserRequestDto);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = _mapper.Map<UserEntity>(request.UserRequestDto);
        var followers = await _followRepository.GetFollowersList(user);
        
        return _mapper.Map<List<UserResponseDto>>(followers);
    }
}