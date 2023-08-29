using System.Diagnostics.Contracts;

namespace Infrastructure.Mail;

public class CloudinaryUrl
{
    public string section { get; set; } = "Cloudinary";
    public string ApiSecete { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
    public string CloudName { get; set; } = null!;
    public string cloudinaryUrl { get; set; } = null!;
}