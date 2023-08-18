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
            RuleFor(x => x);        
        }
    }
}
