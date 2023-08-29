using Application.Contracts.Services;
using CloudinaryDotNet.Actions;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FileUpload : ControllerBase
{
    private readonly IFileUploader _fileUploader;

    public FileUpload(IFileUploader fileUploader)
    {
        _fileUploader = fileUploader;
    }

    [HttpPost("image")]
    public async Task<ImageUploadResult?> UploadImage(List<IFormFile> files)
    {
        ImageUploadResult uploadResult = null;
        foreach (var file in files)
        {
            uploadResult = await _fileUploader.UploadImage(file, "images");
            Console.WriteLine(file.FileName + " " + file.Name + " "  + uploadResult.Url);
        }
        return uploadResult;
    }
}