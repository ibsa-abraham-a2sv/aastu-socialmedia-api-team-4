using Application.Contracts;
using FluentValidation;

namespace Application.Features.UserConnection.Commands.DeleteUserConnection;

public class DeleteUserConnectionValidator : AbstractValidator<DeleteUserConnectionCommand>
{
    public DeleteUserConnectionValidator(IUserConnectionRepository userConnectionRepository)
    {
        RuleFor(u => u.FindUserConnectionDto.UserId)
            .MustAsync(async (userId, token) =>
            {
                var userExists = await userConnectionRepository.ExistsAsync(userId);

                return userExists;
            }).WithMessage("Connection doesn't exist.");
    }
}