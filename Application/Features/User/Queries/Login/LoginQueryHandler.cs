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

namespace Application.Features.User.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(UserManager<UserEntity> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }
        public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var validator = new LoginQueryValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                  throw new ValidationException(validationResult.Errors);
            }

            var user = await _userManager.FindByEmailAsync(request.User.Email);
            if (user == null)
            {
                throw new Exception("User with this email doesn't exist");
            }

            var authres = await _userRepository.Login(request.User);
            return authres;
        }
    }
}
