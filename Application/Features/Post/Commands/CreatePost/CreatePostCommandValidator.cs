
using Application.Contracts;
using FluentValidation;

namespace Application.Features.Post.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public IUserRepository UserRepository {get; set;}
    public CreatePostCommandValidator()
    {   
        
        RuleFor(x => x.NewPost)
        .NotNull()
        .When(x => x.NewPost != null); // Apply rules only if NewPost is not null

        When(x => x.NewPost != null, () =>
        {
            RuleFor(x => x.NewPost.UserId)
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .GreaterThan(0).WithMessage("{PropertyName} can't be less than 1")
                .MustAsync(UserIdExists).WithMessage("Invalid {PropertyName}");

            RuleFor(x => x.NewPost.Title)
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .Length(2, 50).WithMessage("{PropertyName} can't be less than 2");

            RuleFor(x => x.NewPost.Content)
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .Length(1, 500).WithMessage("{PropertyName} can't be less than 1");
        });
        
    }

    private async Task<bool> UserIdExists(int UserId, CancellationToken token)
    {
         var userExists = await UserRepository.GetByIdAsync(UserId);
        return userExists != null;
    }
}