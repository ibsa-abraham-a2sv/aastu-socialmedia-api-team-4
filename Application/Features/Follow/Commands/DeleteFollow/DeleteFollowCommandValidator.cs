using Application.Contracts;
using Application.DTOs.Follow;
using FluentValidation;

namespace Application.Features.Follow.Commands.DeleteFollow;

public class DeleteFollowCommandValidator : AbstractValidator<FollowDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IFollowRepository _followRepository;

    public DeleteFollowCommandValidator(IUserRepository userRepository, IFollowRepository followRepository)
    {
        _userRepository = userRepository;
        _followRepository = followRepository;

        RuleFor(f => f.FollowingId)
            .NotNull().WithMessage("Message must NOT be null.")
            .MustAsync(async (following, cancellationToken) =>
            {
                var userExists = await _userRepository.Exists(following);

                return userExists;
            })
            .WithMessage("{PropertyName} does not exist.");
            
        RuleFor(f => f.FollowerId)
            .NotNull().WithMessage("Message must NOT be null.")
            .MustAsync(async (follower, cancellationToken) =>
            {
                var userExists = await _userRepository.Exists(follower);

                return userExists;
            })
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(f => f)
            .MustAsync(async (followDto, token) =>
            {
                var followExists = await _followRepository.FollowExists(followDto.FollowerId, followDto.FollowingId);

                return followExists;
            })
            .WithMessage("{PropertyName} doesn't exist");
    }
}