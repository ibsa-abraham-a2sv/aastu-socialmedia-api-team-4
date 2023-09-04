
using Application.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Post.Commands.UpdatePost;

public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public IUserRepository _userRepository {get; set;}
    public UpdatePostCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        RuleFor(x => x.UpdatePost)
        .NotNull()
        .When(x => x.UpdatePost != null); // Apply rules only if UpdatePost is not null

        When(x => x.UpdatePost != null, () =>
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .MustAsync(UserIdExists).WithMessage("Invalid {PropertyName}");

            RuleFor(x => x.UpdatePost.Title)
                .Length(1, 50).WithMessage("{PropertyName} can't be less than 1");

            RuleFor(x => x.UpdatePost.Content)
                .Length(1, 500).WithMessage("{PropertyName} can't be less than 1");
            
            RuleFor(x => x.UpdatePost.PictureFile)
                .Must(BeAValidImageFile).WithMessage("Invalid image file.");
        });
    }

    private async Task<bool> UserIdExists(int UserId, CancellationToken token)
    {
        return await _userRepository.Exists(UserId);
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