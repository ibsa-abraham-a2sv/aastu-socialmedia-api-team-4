
using FluentValidation;

namespace Application.Features.Post.Commands.UpdatePost;

public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(x => x.UpdatePost)
        .NotNull()
        .When(x => x.UpdatePost != null); // Apply rules only if UpdatePost is not null

        When(x => x.UpdatePost != null, () =>
        {
            RuleFor(x => x.UpdatePost.UserId)
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .GreaterThan(0).WithMessage("{PropertyName} can't be less than 1");

            RuleFor(x => x.UpdatePost.Title)
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .Length(2, 50).WithMessage("{PropertyName} can't be less than 2");

            RuleFor(x => x.UpdatePost.Content)
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .Length(1, 500).WithMessage("{PropertyName} can't be less than 1");
        });
    }
}