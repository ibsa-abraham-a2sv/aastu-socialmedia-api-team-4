using Application.Contracts;
using Application.DTOs.Follow;
using FluentValidation;

namespace Application.Features.Follow.Commands.CreateFollow;

public class CreateFollowCommandValidator : AbstractValidator<FollowDto>
{
    private readonly IUserRepository _userRepository;

    public CreateFollowCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(f => f.Following)
            .MustAsync(async (following, cancellationToken) =>
            {
                var userExists = await _userRepository.Exists(following);

                return !userExists;
            })
            .WithMessage("{PropertyName} does not exist.");
            
        RuleFor(f => f.FollowerId)    
            .MustAsync(async (follower, cancellationToken) =>
            {
                var userExists = await _userRepository.Exists(follower);

                return !userExists;
            })
            .WithMessage("{PropertyName} does not exist.");
    }
}