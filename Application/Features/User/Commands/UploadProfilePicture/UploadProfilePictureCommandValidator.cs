using FluentValidation;

namespace Application.Features.User.Commands.UploadProfilePicture;

public class UploadProfilePictureCommandValidator : AbstractValidator<UploadProfilePictureCommand>
{
    public UploadProfilePictureCommandValidator()
    {
        RuleFor(x => x.Photo).NotNull().WithMessage("Photo is required");
        RuleFor(x => x.Photo.Length).GreaterThan(0).WithMessage("Photo is required");
    }
}