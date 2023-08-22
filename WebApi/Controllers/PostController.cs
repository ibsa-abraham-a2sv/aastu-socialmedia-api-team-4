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

        //[HttpGet]
        //[Route("SearchPost/{query:string}")]
        //public async Task<ActionResult<IReadOnlyList<PostResponseDto>>> SearchPost(string query)
        //{
        //    var posts = await _mediator.Send(new SearchPostRequest {Query = query});
        //    return Ok(posts);
        //}

        [HttpGet]
        [Route("GetPostsByUserId/{UserId:int}")]
        public async Task<ActionResult<IReadOnlyList<PostResponseDto>>> GetPostsByUserId(int userId)
        {
            var posts = await _mediator.Send(new GetAllPostsByUserIdRequest{UserId = userId});
            return Ok(posts);
        }


        [HttpPost]
        [Route("CreatePost")]
        public async Task<ActionResult<PostResponseDto>> CreatePost(PostRequestDto PostRequest)
        {
            var command = new CreatePostCommand{NewPost = PostRequest};
            var Post = await _mediator.Send(command);

            // return CreatedAtAction(nameof(GetSinglePost), new{Id = Post.Id}, Post);
            return Ok("created");
        }

        [HttpPut]
        [Route("UpdatePost/{id:int}")]
        public async Task UpdatePost(int Id, PostRequestDto PostRequest)
        {
            var command = new UpdatePostCommand
            {
                PostId = Id,
                UpdatePost = PostRequest
            };

            await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("DeletePost/{id:int}")]
        public async Task DeletePost(int id)
        {
            await _mediator.Send(new DeletePostCommand { PostId = id });
        }

    }
}