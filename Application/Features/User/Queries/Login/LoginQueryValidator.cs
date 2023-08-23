using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.User).NotNull().WithMessage("User data is required");
            RuleFor(x => x.User.Email).NotNull().WithMessage("Email is required").EmailAddress().WithMessage("Email field is email address type");
            RuleFor(x => x.User.Password).NotNull().WithMessage("Password is required").MinimumLength(8).WithMessage("Password must be at least 8 characters.");
        }
    }
}
