using Application.Features.Comment.Queries.GetAllCommets.GetAllCommetsByPostId;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Queries.GetAllCommetsGetAllCommetsByPostId
{
    public class GetCommentByPostIdQueryValidator : AbstractValidator<GetAllCommentsByPostIdQuery>
    {
        public GetCommentByPostIdQueryValidator()
        {
            RuleFor(x => x.PostId).NotEmpty().WithMessage("{PropertyName} is required").GreaterThan(0).WithMessage("{PropertyName} can't be less than 1");
        }
    }
}
