using System.Security.Claims;
using Application.DTOs.Post;
using Application.Features.Post.Commands.CreatePost;
using Application.Features.Post.Commands.DeletePost;
using Application.Features.Post.Commands.UpdatePost;
using Application.Features.Post.Queries.GetAllPosts;
using Application.Features.Post.Queries.GetAllPostsByUserId;
using Application.Features.Post.Queries.GetSinglePost;
using Application.Features.Post.Queries.SearchPost;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Exceptions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IReadOnlyList<PostResponseDto>>> GetAll()
        {
            var posts = await _mediator.Send(new GetAllPostsRequest());
            return Ok(posts);
        }

        [HttpGet]
        [Route("GetSinglePost/{postId:int}")]
        public async Task<ActionResult<PostResponseDto>> GetSinglePost(int postId)
        {
            return await _mediator.Send(new GetSinglePostRequest{PostId = postId});
        }

        [HttpGet]
        [Route("SearchPost/{query}")]
        public async Task<ActionResult<IReadOnlyList<PostResponseDto>>> SearchPost(string query)
        {
           var posts = await _mediator.Send(new SearchPostRequest {Query = query});
           return Ok(posts);
        }

        [HttpGet]
        [Route("GetPostsByUserId/{UserId:int}")]
        public async Task<ActionResult<IReadOnlyList<PostResponseDto>>> GetPostsByUserId(int userId)
        {
            var posts = await _mediator.Send(new GetAllPostsByUserIdRequest{UserId = userId});
            return Ok(posts);
        }


        [HttpPost]
        [Route("CreatePost")]
        public async Task<ActionResult<PostResponseDto>> CreatePost(PostRequestDto postRequest)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }
            
            var userId = int.Parse(userIdClaim.Value);
            
            if (userId != postRequest.UserId)
            {
                throw new Exception("User not authorized");
            }
            var command = new CreatePostCommand{NewPost = postRequest};
            var Post = await _mediator.Send(command);


            return CreatedAtAction(nameof(GetSinglePost), new{Id = Post.Id}, Post);
        }

        [HttpPut]
        [Route("UpdatePost/{id:int}")]
        public async Task<ActionResult> UpdatePost(int id, PostRequestDto PostRequest)
        {
            var post = await _mediator.Send(new GetSinglePostRequest{PostId = id});
            
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }
            
            var userId = int.Parse(userIdClaim.Value);
            
            if (userId != post.UserId)
            {
                throw new Exception("User not authorized");
            }
            
            var command = new UpdatePostCommand
            {
                PostId = id,
                UpdatePost = PostRequest
            };

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [Route("DeletePost/{id:int}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            var post = await _mediator.Send(new GetSinglePostRequest{PostId = id});
            
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }
            
            var userId = int.Parse(userIdClaim.Value);
            
            if (userId != post.UserId)
            {
                throw new Exception("User not authorized");
            }
            
            await _mediator.Send(new DeletePostCommand { PostId = id });
            return NoContent();
        }

    }
}
