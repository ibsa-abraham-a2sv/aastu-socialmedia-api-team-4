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

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtsettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtsettings);
            services.AddSingleton(Options.Create(jwtsettings));

            Console.WriteLine("1", jwtsettings.Audience);
            Console.WriteLine(jwtsettings.Issuer);
            Console.WriteLine(jwtsettings.Secret);
            Console.WriteLine(jwtsettings.ExpiryMinutes);

            //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("SocialMediaDB"));
            });
           
            services.AddIdentity<UserEntity, IdentityRole<int>>( options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;

                options.User.RequireUniqueEmail = true;
                //options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();


            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IUserConnectionRepository, UserConnectionRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
