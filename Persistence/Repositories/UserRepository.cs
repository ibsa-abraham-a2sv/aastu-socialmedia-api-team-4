using Application.Contracts;
using Application.DTOs.User;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence.Repositories.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly AppDBContext _dbContext;
        private readonly JwtSettings _jwtSettings;
        public UserRepository(AppDBContext dbContext, IOptions<JwtSettings> jwtSettings) : base(dbContext)
        {
            _dbContext = dbContext;
            _jwtSettings = jwtSettings.Value;
        }

        // public async Task<AuthResponse> Login(UserRequestDto userDto)
        // {
        //     Console.WriteLine(_jwtSettings.Audience);
        //     Console.WriteLine(_jwtSettings.Issuer);
        //     Console.WriteLine(_jwtSettings.Secret);
        //     Console.WriteLine(_jwtSettings.ExpiryMinutes);
        //     var result = await _signInManager.PasswordSignInAsync(userDto.UserName, userDto.Password, false, false);
        //     if (!result.Succeeded)
        //     {
        //         throw new Exception(result.ToString());
        //     }
        //
        //     // generate jwt token
        //     var user = await _userManager.FindByEmailAsync(userDto.Email);
        //     var token = JwtTokenGenerator.GenerateToken(user.Email, user.UserName, user.Id, user.FirstName, user.LastName, _jwtSettings);
        //     
        //     AuthResponse response = new AuthResponse
        //     {
        //         UserName = user.UserName,
        //         FirstName = user.FirstName,
        //         LastName = user.LastName,
        //         Email = user.Email,
        //         Token = token
        //     };
        //     return response;
        // }
        // public async Task<UserEntity> Register(UserEntity request)
        // {
        //     var createdUser = await _dbContext.Users.AddAsync(request);
        //     return createdUser;
        // }
        public async Task<AuthResponse> Login(UserRequestDto user)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> Register(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity?> GetUserByEmail(string userEmail)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x!.Email == userEmail);
        }
    }
}
