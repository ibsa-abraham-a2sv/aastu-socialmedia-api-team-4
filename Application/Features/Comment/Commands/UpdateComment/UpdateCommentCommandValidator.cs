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
            When(dto => dto.UpdateCommentDTO != null && !string.IsNullOrEmpty(dto.UpdateCommentDTO.Text), () =>
            {
                RuleFor(x => x.UpdateCommentDTO.Text).MinimumLength(10).WithMessage("{PropertyName} must either empty or length of greater than 10");
            });
        }
    }
}
