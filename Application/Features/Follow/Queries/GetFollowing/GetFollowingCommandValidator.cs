using Application.Contracts;
using FluentValidation;

namespace Application.Features.Follow.Queries.GetFollowing;

public class GetFollowingCommandValidator : AbstractValidator<GetFollowingCommand>
{
    private readonly IUserRepository _userRepository;

    public GetFollowingCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
        RuleFor(u => u.UserId)
            .MustAsync(async (id, cancellationToken) =>
            {
                var userExists = await _userRepository.Exists(id);

                return userExists;
            })
            .WithMessage("{PropertyName} does not exist.");
    }
}