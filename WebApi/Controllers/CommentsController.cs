using System.Security.Claims;
using Application.DTOs.Comment;
using Application.Features.Comment.Commands.CreateComment;
using Application.Features.Comment.Commands.DeleteComment;
using Application.Features.Comment.Commands.UpdateComment;
using Application.Features.Comment.Queries.GetAllCommets;
using Application.Features.Comment.Queries.GetAllCommets.GetAllCommetsByPostId;
using Application.Features.Comment.Queries.GetOneComment;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using WebApi.Service;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<CommentResponseDTO>>> Get()
        {
            return await _mediator.Send(new GetAllCommentsQuery());
        }

        [HttpGet("ofPost/{postId}")]
        public async Task<ActionResult<List<CommentResponseDTO>>> GetCommentOfPost(int postId)
        {
            return await _mediator.Send(new GetAllCommentsByPostIdQuery
            {
                PostId = postId
            });
        }

        [HttpGet("One/{id:int}")]
        public async Task<ActionResult<CommentResponseDTO>> Get(int id)
        {
            return await _mediator.Send(new GetOneCommentQuery
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<ActionResult<CommentResponseDTO>> Post(CommentRequestDto commentRequest)
        {
            var userId = await AuthHelper.GetUserId(User);
            
            var command = new CreateCommentCommand
            {
                userId = userId,
                commentRequestDTO = commentRequest
            };
            var comment = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), comment.Id, comment);
        }

        [HttpPut("{id:int}")]
        public async Task Update(int id, CommentRequestDto commentRequest)
        {
            var comment = await _mediator.Send( new GetOneCommentQuery { Id = id });
            await AuthHelper.CheckUserById(User, comment.UserId);
            
            var command = new UpdateCommentCommand
            {
                Id = id,
                UpdateCommentDto = commentRequest
            };

            await _mediator.Send(command);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            var comment = await _mediator.Send( new GetOneCommentQuery { Id = id });
            await AuthHelper.CheckUserById(User, comment.UserId);
            
            await _mediator.Send(new DeleteCommentCommand { Id = id });
        }

    }
}
