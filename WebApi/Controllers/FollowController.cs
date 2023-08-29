using System.Security.Claims;
using Application.DTOs.Follow;
using Application.DTOs.User;
using Application.Features.Follow.Commands.CreateFollow;
using Application.Features.Follow.Commands.DeleteFollow;
using Application.Features.Follow.Queries.GetFollowers;
using Application.Features.Follow.Queries.GetFollowing;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Service.NotificationService;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class FollowController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly INotificationService _notificationService;

    public FollowController(IMediator mediator, INotificationService notificationService)
    {
        _mediator = mediator;
        _notificationService = notificationService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateFollow(int followingUserId)
    {
        //todo: change the logged in user fetching to modular one
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            throw new Exception("User not authenticated");
        }
            
        var loggedInUser = int.Parse(userIdClaim.Value);
        
        // check if user is trying to follow himself
        if (loggedInUser == followingUserId)
            return StatusCode(403, "You cannot follow yourself.");

        var followDto = new FollowDto
        {
            FollowerId = loggedInUser,
            FollowingId = followingUserId
        };

        var command = new CreateFollowCommand
        {
            FollowDto = followDto
        };

        var follow = await _mediator.Send(command);

        if (follow != null)
        {
            await _notificationService.UserIsFollowed(loggedInUser, followingUserId);
        }

        return Ok(follow);
    }
    
    [HttpPost]
    [Route("delete")]
    public async Task<IActionResult> DeleteFollow(int followingUserId)
    {
        //todo: change the logged in user fetching to modular one
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            throw new Exception("User not authenticated");
        }
            
        var loggedInUser = int.Parse(userIdClaim.Value);
        
        // check if user is trying to follow himself
        if (loggedInUser == followingUserId)
                return StatusCode(403, "You can NOT input your own self.");
    
        var followDto = new FollowDto
        {
            FollowerId = loggedInUser,
            FollowingId = followingUserId
        };
    
        var command = new DeleteFollowCommand
        {
            FollowDto = followDto
        };
    
        var follow = await _mediator.Send(command);
    
        return !follow ? StatusCode(500, "Server error has occured.") : Ok();
    }

    [HttpGet]
    [Route("getFollowers")]
    public async Task<ActionResult<List<UserResponseDto>>> GetFollowersList()
    {
        //todo: change the logged in user fetching to modular one
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            throw new Exception("User not authenticated");
        }
            
        var loggedInUser = int.Parse(userIdClaim.Value);

        var command = new GetFollowersCommand
        {
            UserId = loggedInUser
        };

        var followersList = await _mediator.Send(command);

        return followersList;
    }

    [HttpGet]
    [Route("getFollowing")]
    public async Task<ActionResult<List<UserResponseDto>>> GetFollowingList()
    {
        //todo: change the logged in user fetching to modular one
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            throw new Exception("User not authenticated");
        }
            
        var loggedInUser = int.Parse(userIdClaim.Value);

        var command = new GetFollowingCommand
        {
            UserId = loggedInUser
        };

        var followingList = await _mediator.Send(command);

        return followingList;
    }
}