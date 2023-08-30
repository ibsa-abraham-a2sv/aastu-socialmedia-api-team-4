using Application.Contracts.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.FileUpload;

public class FileUploader : IFileUploader
{
    private readonly Cloudinary _cloudinary;

    public FileUploader(IOptions<CloudinaryUrl> cloudinaryUrl)
    {
        // cloudinary configuration
        var cloudinaryUrl1 = cloudinaryUrl.Value;
        _cloudinary = new Cloudinary(cloudinaryUrl1.cloudinaryUrl);
    }

    public async Task<ImageUploadResult> UploadImage(IFormFile file, string folderName)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = folderName,
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult;
    }

    public Task<ImageUploadResult> UploadVideo(IFormFile file, string folderName)
    {
        throw new NotImplementedException();
    }
}