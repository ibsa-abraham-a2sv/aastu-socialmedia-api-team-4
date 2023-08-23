using Application.Contracts;
using FluentValidation;

namespace Application.Features.UserConnection.Commands;

public class CreateUserConnectionCommandValidator : AbstractValidator<CreateUserConnectionCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserConnectionCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(u => u.CreateUserConnectionDto.UserId)
            .MustAsync(async (userId, token) =>
            {
                var userExists = await _userRepository.Exists(userId);

                return userExists;
            }).WithMessage("User does not exist.");
    }
}