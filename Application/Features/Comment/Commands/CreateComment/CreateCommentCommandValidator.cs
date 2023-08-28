using FluentValidation;

namespace Application.Features.Comment.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator() {
            RuleFor(x => x.commentRequestDTO.PostId).NotEmpty().WithMessage("{PropertyName} is Required").GreaterThan(0).WithMessage("{PropertyName} can't be less than 1");

            RuleFor(x => x.userId).NotEmpty().WithMessage("{PropertyName} is Required").GreaterThan(0).WithMessage("{PropertyName} can't be less than 1");

            RuleFor(x => x.commentRequestDTO.Text).NotEmpty().WithMessage("{PropertyName} is Required").Length(1, 500).WithMessage("{PropertyName} can't be less than 1");
        }
    }
}
