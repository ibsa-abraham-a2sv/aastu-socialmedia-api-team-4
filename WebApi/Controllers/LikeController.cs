using System.Security.Claims;
using Application.DTOs.Like;
using Application.Features.Like.Commands.Create_Like;
using Application.Features.Like.Commands.Delete_Like;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class LikeController : ControllerBase
{
    private readonly IMediator _mediator;

    public LikeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<LikeDto>> CreateLike(int postId)
    {
        //todo: change the logged in user fetching to modular one
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            throw new Exception("User not authenticated");
        }
            
        var userId = int.Parse(userIdClaim.Value);
            
        // if (userId != likeDto.UserId)
        // {
        //     throw new Exception("User not authorized");
        // }
        var command = new CreateLikeCommand
        {
            LikeDto = new LikeDto
            {
                UserId = userId,
                PostId = postId
            }
        };

        var like = await _mediator.Send(command);

        return CreatedAtAction(null, like);
    }

    [HttpPost("delete")]
    public async Task<ActionResult<Unit>> DeleteLike(int postId)
    {
        var command = new DeleteLikeCommand
        {
            PostId = postId
        };

        var result = await _mediator.Send(command);

        return Ok();
    }
}