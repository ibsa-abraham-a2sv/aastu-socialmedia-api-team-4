using FluentValidation;

namespace Application.Features.Like.Commands.Create_Like;

public class CreateLikeCommandValidator : AbstractValidator<CreateLikeCommand>
{
    public CreateLikeCommandValidator()
    {
        RuleFor(x => x.LikeDto.PostId)
            .NotEmpty()
            .WithMessage("{PropertyName} is Required")
            .GreaterThan(0)
            .WithMessage("{PropertyName} can't be less than 1");
        RuleFor(x => x.LikeDto.UserId)
            .NotEmpty()
            .WithMessage("{PropertyName} is Required")
            .GreaterThan(0)
            .WithMessage("{PropertyName} can't be less than 1");
    }
}