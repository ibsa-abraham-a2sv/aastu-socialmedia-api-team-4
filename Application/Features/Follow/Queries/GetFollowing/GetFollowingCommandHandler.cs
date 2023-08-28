using Application.Contracts;
using Application.DTOs.User;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Features.Follow.Queries.GetFollowing;

public class GetFollowingCommandHandler : IRequestHandler<GetFollowingCommand, List<UserResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IFollowRepository _followRepository;
    private readonly IMapper _mapper;

    public GetFollowingCommandHandler(IUserRepository userRepository, IFollowRepository followRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _followRepository = followRepository;
        _mapper = mapper;
    }
    
    public async Task<List<UserResponseDto>> Handle(GetFollowingCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetFollowingCommandValidator(_userRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var following = await _followRepository.GetFollowingList(request.UserId);

        return _mapper.Map<List<UserResponseDto>>(following);
    }
}