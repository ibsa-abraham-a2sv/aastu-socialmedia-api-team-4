
using Application.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Post.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.NewPost)
        .NotNull()
        .When(x => x.NewPost != null); // Apply rules only if NewPost is not null

        When(x => x.NewPost != null, () =>
        {
            RuleFor(x => x.NewPost.Title)
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .Length(1, 50).WithMessage("{PropertyName} can't be less than 1");

            RuleFor(x => x.NewPost.Content)
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .Length(1, 500).WithMessage("{PropertyName} can't be less than 1");

            RuleFor(x => x.NewPost.PictureFile)
                .Must(BeAValidImageFile).WithMessage("Invalid image file.");
        });
        
    }
    private bool BeAValidImageFile(IFormFile file)
    {
        if (file == null)
            return true;

        // Define a list of allowed content types and file extensions for images
        var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

        // Check if the content type and file extension are valid
        if (!allowedContentTypes.Contains(file.ContentType) || 
            !allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
        {
            return false;
        }

        return true;
    }
}