using Application.DTOs.Like;
using Application.Features.Comment.Commands.DeleteComment;
using Application.Features.Like.Commands.Create_Like;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LikeController : ControllerBase
{
    private readonly IMediator _mediator;

    public LikeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<LikeDto>> CreateLike(LikeDto likeDto)
    {
        var command = new CreateLikeCommand
        {
            LikeDto = likeDto
        };

        var like = await _mediator.Send(command);

        return CreatedAtAction(null, like);
    }

    [HttpPost("delete/{likeId:int}")]
    public async Task<ActionResult<Unit>> DeleteLike(int likeId)
    {
        var command = new DeleteCommentCommand
        {
            Id = likeId
        };

        var result = await _mediator.Send(command);

        return NoContent();
    }
}