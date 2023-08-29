using Application.Contracts.Infrastructure;
using Application.Contracts.Services;
using Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CloudinaryUrl>(configuration.GetSection("Cloudinary"));
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped<IFileUploader, FileUploader>();
        services.AddSingleton<IConfiguration>(configuration);
        return services;
    }
}