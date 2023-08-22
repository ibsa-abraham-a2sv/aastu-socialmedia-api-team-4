using Application.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.SignalR;

namespace Application.Features.User.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    
    public DeleteUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
        //Rule for UserID
        RuleFor(x => x.UserID)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MustAsync(async (id, token) =>
            {
                var userExists = await _userRepository.Exists(id);
                return !userExists;
            })
            .WithMessage("{PropertyName} can't be less than 1");
    }
}