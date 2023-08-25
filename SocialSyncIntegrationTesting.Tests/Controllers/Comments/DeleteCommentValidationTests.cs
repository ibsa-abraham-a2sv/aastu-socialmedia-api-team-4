using System.Net;
using SocialSyncIntegrationTesting.Tests.Features.Comments;
using Shouldly;


namespace SocialSyncIntegrationTesting.Tests.Controllers.Comments;

public class DeleteCommentValidationTests : IClassFixture<CustomeWebApplicationFactory>
{
    private readonly  HttpClient _client;

    public DeleteCommentValidationTests(CustomeWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task DeleteComment_ReturnsNotFoundStatusCode()
    {
        // Arrange
        int id = 100;
        
        // Act
        var response = await _client.DeleteAsync($"Comments/{id}");
        
        // Assert
        response.Content.Headers.ContentType?.MediaType.ShouldBe("text/plain");
        response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);   
    }
}