using Application.DTOs.User;
using Application.Features.User.Commands.CreateUser;
using Application.Features.User.Queries.GetAllUsers;
using Application.Features.User.Queries.GetSingleUser;
using Application.Features.User.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController
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
        [Authorize]
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

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(UserRequestDto query)
        {
            var response = await _mediator.Send(new LoginQuery {  User = query});
            return response;
        }
    }
}
