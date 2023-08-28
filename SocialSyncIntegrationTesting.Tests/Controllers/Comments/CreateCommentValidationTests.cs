using System.Net;
using System.Text;
using Application.DTOs.Comment;
using Newtonsoft.Json;
using SocialSyncIntegrationTesting.Tests.Features.Comments;
using Shouldly;

namespace SocialSyncIntegrationTesting.Tests.Controllers.Comments;

public class CreateCommentValidationTests : IClassFixture<CustomeWebApplicationFactory>
{
    private readonly CustomeWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public CreateCommentValidationTests(CustomeWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }
    
    [Fact]
    public async Task CreateComments_ReturnsInternalServerErrorStatusCode()
    {
        // Arrange
        var comments = new []{
            new CommentRequestDTO
            {
                UserId = 2,
                PostId = 1
            },
            new CommentRequestDTO
            {
                Text = "This is a comment",
                PostId = 1
            },
            new CommentRequestDTO
            {
                Text = "This is a comment",
                UserId = 3,
            },
        };
        
        for (int i = 0; i < 3; i++)
        {
            var json = JsonConvert.SerializeObject(comments[i]);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
    
            // Act
            var response = await _client.PostAsync("/Comments", data);
            var errorResponse = await response.Content.ReadAsStringAsync();
        
            // here I want to parse the errors ans assert them
        
            // Assert
            response.Content.Headers.ContentType?.MediaType.ShouldBe("text/plain");
            response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);   
        }
    }
}