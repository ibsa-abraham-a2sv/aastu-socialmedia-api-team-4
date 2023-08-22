using Application.Contracts;
using Application.DTOs.User;
using FluentValidation;

namespace Application.Features.Follow.Queries.GetFollowers;

public class GetFollowersCommandValidator : AbstractValidator<GetFollowersCommand>
{
    private readonly IUserRepository _userRepository;

    public GetFollowersCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
        RuleFor(u => u.UserId)
            .MustAsync(async (id, cancellationToken) =>
            {
                var userExists = await _userRepository.Exists(id);

                return !userExists;
            })
            .WithMessage("{PropertyName} does not exist.");
    }
}