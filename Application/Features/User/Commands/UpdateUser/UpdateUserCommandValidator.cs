using Application.Contracts;
using FluentValidation;

namespace Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
        //Rule for FirstName
        RuleFor(u => u.UserDto.FirstName)
            .Must(str => str.Length is 0 or >= 2 and <= 20)
            .WithMessage("{PropertyName} length must be between 2 and 20 characters.")
            .Matches("^[a-zA-Z0-9_]+$")
            .WithMessage("{PropertyName} must contain only alphanumeric characters and underscores.");
        
        //Rule for LastName
        RuleFor(u => u.UserDto.LastName)
            .Must(str => str.Length is 0 or >= 2 and <= 20)
            .WithMessage("{PropertyName} must be between 2 and 20 characters.")
            .Matches("^[a-zA-Z0-9_]+$")
            .WithMessage("{PropertyName} must contain only alphanumeric characters and underscores.");
        
        //Rule for Bio
        RuleFor(u => u.UserDto.Bio)
            .MaximumLength(500).WithMessage("{PropertyName} must be less than 500 characters.");
        
        //Rule for DateOfBirth
        RuleFor(u => u.UserDto.DateOfBirth)
            .LessThan(DateTime.Now).WithMessage("{PropertyName} must be less than today's date.");
        
    }
}