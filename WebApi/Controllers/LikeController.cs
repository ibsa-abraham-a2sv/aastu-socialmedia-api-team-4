using System.Security.Claims;
using Application.DTOs.Like;
using Application.Features.Like.Commands.Create_Like;
using Application.Features.Like.Commands.Delete_Like;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Service;
using WebApi.Service.NotificationService;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class LikeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly INotificationService _notificationService;

    public LikeController(IMediator mediator, INotificationService notificationService)
    {
        _mediator = mediator;
        _notificationService = notificationService;
    }

    [HttpPost]
    public async Task<ActionResult<LikeDto>> CreateLike(int postId)
    {
        var userId = await AuthHelper.GetUserId(User);
            
        var command = new CreateLikeCommand
        {
            LikeDto = new LikeDto
            {
                UserId = userId,
                PostId = postId
            }
        };

        var like = await _mediator.Send(command);

        
            await _notificationService.PostIsLiked(postId, userId);

        return CreatedAtAction(null, like);
    }

    [HttpPost("delete")]
    public async Task<ActionResult<Unit>> DeleteLike(int postId)
    {
        var loggedInUser = await AuthHelper.GetUserId(User);
        
        var command = new DeleteLikeCommand
        {
            PostId = postId,
            UserId = loggedInUser
        };

        var result = await _mediator.Send(command);

        return Ok();
    }
}