using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Queries.GetOneComment
{
    public class GetOneCommentQueryValidator : AbstractValidator<GetOneCommentQuery>{
        public GetOneCommentQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} is required").GreaterThan(0).WithMessage("{PropertyName} can't be less than 1");
        }
    }
}
