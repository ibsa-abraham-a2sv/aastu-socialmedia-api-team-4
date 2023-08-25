using Application.Contracts;
using Application.Contracts.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Repositories;
using Persistence.Repositories.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSettings jwtsettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtsettings);
            services.AddSingleton(Options.Create(jwtsettings));

            //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("SocialMediaDB"));
            });
            
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IUserConnectionRepository, UserConnectionRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtsettings.Issuer,
                ValidAudience = jwtsettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsettings.Secret)),
            });
            return services;
        }
    }
}
