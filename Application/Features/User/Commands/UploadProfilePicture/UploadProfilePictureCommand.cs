using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.User.Commands.UploadProfilePicture;

public class UploadProfilePictureCommand : IRequest<bool>
{
    public int UserId { set; get; }
    public IFormFile Photo { set; get; } = null!;
}