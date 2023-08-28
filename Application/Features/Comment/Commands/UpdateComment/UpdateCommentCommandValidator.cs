using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Commands.UpdateComment
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator() 
        {
            When(dto => !string.IsNullOrEmpty(dto.UpdateCommentDto.Text), () =>
            {
                RuleFor(x => x.UpdateCommentDto.Text).MinimumLength(10).WithMessage("{PropertyName} must either empty or length of greater than 10");
            });
            
            When(dto => dto.UpdateCommentDto.PostId != 0, () =>
            {
                RuleFor(x => x.UpdateCommentDto.PostId).GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater or equal to than 1");
            });
        }
    }
}
