using Application.Contracts;
using Application.DTOs.User;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Common;

namespace Application.Features.User.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var validator = new LoginQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                  throw new ValidationException(validationResult.Errors);
            }
            
            var user = await _userRepository.GetUserByEmail(request.User.Email);
            if (user == null)
            {
                throw new Exception("Invalid Credentials");
            }
            // verify password with bcrypt
            var result = BCrypt.Net.BCrypt.Verify(request.User.Password, user.Password);
            if (!result)
            {
                throw new Exception("Invalid Credentials");
            }
            
            // generate jwt token
            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.UserName, user.FirstName, user.LastName);
            var authres = new AuthResponse
            {
                Token = token,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName
            };
            return authres;
        }
    }
}
