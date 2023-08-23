using Application.Contracts;
using FluentValidation;

namespace Application.Features.UserConnection.Queries.GetUserConnection;

public class GetUserConnectionValidator : AbstractValidator<GetUserConnection>
{
    public GetUserConnectionValidator(IUserConnectionRepository userConnectionRepository)
    {
        RuleFor(u => u.FindUserConnectionDto.UserId)
            .MustAsync(async (userId, token) =>
            {
                var userExists = await userConnectionRepository.ExistsAsync(userId);

                return userExists;
            }).WithMessage("Connection doesn't exist.");
    }
}