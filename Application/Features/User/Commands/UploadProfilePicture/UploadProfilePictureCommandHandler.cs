using System.Net;
using Application.Contracts;
using Application.Contracts.Services;
using FluentValidation;
using MediatR;

namespace Application.Features.User.Commands.UploadProfilePicture;

public class UploadProfilePictureCommandHandler : IRequestHandler<UploadProfilePictureCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IFileUploader _fileUploader;
    
    public UploadProfilePictureCommandHandler(IUserRepository userRepository, IFileUploader fileUploader)
    {
        _userRepository = userRepository;
        _fileUploader = fileUploader;
    }

    public async Task<bool> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var validator = new UploadProfilePictureCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var user = await _userRepository.GetByIdAsync(request.UserId);
        var uploadResult = await _fileUploader.UploadImage(request.Photo, user.UserName);
        
        if (uploadResult.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception("Problem uploading image");
        }

        return true;
    }
}