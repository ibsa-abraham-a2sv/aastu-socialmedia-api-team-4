using System.Net;
using System.Net.Http.Json;
using System.Text;
using Application.DTOs.Comment;
using Newtonsoft.Json;
using SocialSyncIntegrationTesting.Tests.Features.Comments;
using Shouldly;

namespace SocialSyncIntegrationTesting.Tests.Controllers.Comments;

public class UpdateCommentValidationTests : IClassFixture<CustomeWebApplicationFactory>
{
    private readonly CustomeWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public UpdateCommentValidationTests(CustomeWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }
    
    [Fact]
    public async Task UpdateComment_ReturnsNotFoundStatusCode()
    {
        // 
        int id = 100;
        var comment = new CommentRequestDTO
        {
            Text = "This is a comment",
            UserId = 3,
            PostId = 1
        };
        
        // Act
        var response = await _client.PutAsync($"Comments/{id}", JsonContent.Create(comment));
        
        // Assert
        response.Content.Headers.ContentType?.MediaType.ShouldBe("text/plain");
        response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);   
    }
    
    [Fact]
    public async Task UpdateComments_ReturnsInternalServerErrorStatusCode()
    {
        // Arrange
        int id = 1;
        var comment = new CommentRequestDTO
        {
            Text = "Test comment",
            UserId = 3,
            PostId = 1
        };
        
        // var json = JsonConvert.SerializeObject(comment);
        // var data = new StringContent(json, Encoding.UTF8, "application/json");
        
        var res = await _client.PostAsJsonAsync($"/Comments", comment);
        res.StatusCode.ShouldBe(HttpStatusCode.Created);
        
        var comments = new []{
            new CommentRequestDTO
            {
                Text = "Comment", // text should be empty or more than 10 characters
                UserId = 2,
                PostId = 1
            },
            new CommentRequestDTO
            {
                Text = "This is a comment",
                PostId = -1 // post id should be greater than 0
            },
            new CommentRequestDTO
            {
                Text = "This is a comment",
                UserId = -3, // user id should be greater than 0
            }    ,
            new CommentRequestDTO(){ // this should be a valid comment to update 
                Text = "Comment of post", 
                UserId = 2,
                PostId = 1
            }
        };
        
        for (int i = 0; i < comments.Length; i++)
        {
            // Arrange
            var json = JsonConvert.SerializeObject(comments[i]);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
    
            // Act
            var response = await _client.PutAsync($"/Comments/{id}", data);
            var errorResponse = await response.Content.ReadAsStringAsync();
        
            // here I want to parse the errors ans assert them
        
            // Assert
            response.Content.Headers.ContentType?.MediaType.ShouldBe("text/plain");

            response.StatusCode.ShouldBe(i == 3 ? HttpStatusCode.OK :HttpStatusCode.InternalServerError);
        }
    }
}