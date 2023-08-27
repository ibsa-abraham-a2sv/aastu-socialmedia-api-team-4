using Application.DTOs.Auth;
using Application.Features.Auth.LogIn;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest authRequest)
    {
        var response = await _mediator.Send(new LoginUserCommand
        {
            AuthRequest = authRequest
        });
        return response;
    }
}