﻿using System.Security.Claims;
using Application.DTOs.Auth;
using Application.DTOs.User;
using Application.Features.User.Commands.CreateUser;
using Application.Features.User.Commands.DeleteUser;
using Application.Features.User.Commands.UpdateUser;
using Application.Features.User.Commands.UpdateUserPassword;
using Application.Features.User.Commands.VerifyUser;
using Application.Features.User.Queries.GetAllUsers;
using Application.Features.User.Queries.GetSingleUser;
// using Application.Features.User.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Service;

// using System.Web;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponseDto>> Get(int id)
        {
            return await _mediator.Send(new GetSingleUserRequest
            {
                Id = id
            });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<UserResponseDto>>> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersRequest());
            return users;
        }

        [HttpPost("register")]
        public async Task<ActionResult<int>> Register(UserRequestDto userDto)
        {
            if (userDto == null)
            {
                throw new Exception("");
            }
            var response = await _mediator.Send(new CreateUserCommand
            {
                UserDto = userDto
            });
            return response;
        }
        
        [HttpGet("verify")]
        public async Task<ActionResult<bool>> VerifyUser([FromQuery] string email, [FromQuery] string token)
        {
            var response = await _mediator.Send(new VerifyUserCommand
            {
                Email = email,
                Token = token
            });
            return response;
        }
        
        [HttpPut("updatePassword")]
        public async Task<ActionResult<Unit>> UpdatePassword(AuthRequest authRequest)
        {
            await AuthHelper.CheckUserByEmail(User, authRequest.Email);
            var response = await _mediator.Send(new UpdateUserPasswordCommand
            {
                 AuthRequest = authRequest
            });
            return response;
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Unit>> UpdateUser(int id, UserUpdateRequestDto userDto)
        {
            await AuthHelper.CheckUserById(User, id);
            
            var response = await _mediator.Send(new UpdateUserCommand
            {
                Id = id,
                UserDto = userDto
            });
            return response;
        }
        
        [HttpDelete("{id:int}")]
        public async Task DeleteUser(int id)
        {
            await AuthHelper.CheckUserById(User, id);
            
            await _mediator.Send(new DeleteUserCommand
            {
                UserID = id
            });
        }
        // [HttpPost("login")]
        // public async Task<ActionResult<AuthResponse>> Login(UserRequestDto query)
        // {
        //     var response = await _mediator.Send(new LoginQuery {  User = query});
        //     return response;
        // }
    }
}
