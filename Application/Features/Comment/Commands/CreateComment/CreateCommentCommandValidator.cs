using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Commands.DeleteComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator() {
            RuleFor(x => x.commentRequestDTO.PostId).NotEmpty().WithMessage("{PropertyName} is Required").GreaterThan(0).WithMessage("{PropertyName} can't be less than 1");

            RuleFor(x => x.commentRequestDTO.UserId).NotEmpty().WithMessage("{PropertyName} is Required").GreaterThan(0).WithMessage("{PropertyName} can't be less than 1");

            RuleFor(x => x.commentRequestDTO.Text).NotEmpty().WithMessage("{PropertyName} is Required").Length(1, 500).WithMessage("{PropertyName} can't be less than 1");
        }
    }
}
