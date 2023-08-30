using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Application.Contracts.Services;

public interface IFileUploader
{
    Task<ImageUploadResult> UploadImage(IFormFile file, string folderName);
    Task<ImageUploadResult> UploadVideo(IFormFile file, string folderName);
}