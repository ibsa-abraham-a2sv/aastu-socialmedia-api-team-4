
using Application.Contracts;
using FluentValidation;

namespace Application.Features.Post.Commands.UpdatePost;

public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public IUserRepository UserRepository {get; set;}
    public UpdatePostCommandValidator()
    {
        RuleFor(x => x.UpdatePost)
        .NotNull()
        .When(x => x.UpdatePost != null); // Apply rules only if UpdatePost is not null

        When(x => x.UpdatePost != null, () =>
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .MustAsync(UserIdExists).WithMessage("Invalid {PropertyName}");

            RuleFor(x => x.UpdatePost.Title)
                .Length(1, 50).WithMessage("{PropertyName} can't be less than 1");

            RuleFor(x => x.UpdatePost.Content)
                .Length(1, 500).WithMessage("{PropertyName} can't be less than 1");
        });
    }

    private async Task<bool> UserIdExists(int UserId, CancellationToken token)
    {
        return await UserRepository.Exists(UserId);
    }
}