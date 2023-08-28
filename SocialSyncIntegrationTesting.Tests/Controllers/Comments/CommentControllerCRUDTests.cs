using System.Net;
using System.Net.Http.Json;
using System.Text;
using Application.DTOs.Comment;
using Newtonsoft.Json;
using Shouldly;
using SocialSyncIntegrationTesting.Tests.Features.Comments;

namespace SocialSyncIntegrationTesting.Tests.Controllers.Comments;

[Collection("Sequential")]
public class CommentControllerCRUDTests : IClassFixture<CustomeWebApplicationFactory>
{
    private readonly CustomeWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public CommentControllerCRUDTests(CustomeWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }
    
    [Fact]
    public async Task CreateComments_ReturnsSuccessStatusCode()
    {
        // Arrange
        for (int i = 0; i < 3; i++)
        {
            
            var comment = new CommentRequestDTO
            {
                Text = "Test comment" + (i + 1).ToString(),
                UserId = 3,
                PostId = 1
            };
            
            var json = JsonConvert.SerializeObject(comment);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
        
            // Act
            var response = await _client.PostAsync("/Comments", data);
        
            // Assert
            response.Content.Headers.ContentType?.MediaType.ShouldBe("application/json");
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            
            // read the response 
            var responseContent = await response.Content.ReadAsStringAsync();
            var commentResponse = JsonConvert.DeserializeObject<CommentResponseDTO>(responseContent);
        
            // assert the content
            commentResponse.ShouldNotBeNull();
            commentResponse.ShouldBeOfType(typeof(CommentResponseDTO));
            commentResponse.Text.ShouldBe(comment.Text);
            commentResponse.UserId.ShouldBe(comment.UserId);
            commentResponse.PostId.ShouldBe(comment.PostId);
            commentResponse.Id.ShouldBe(i + 1);
        }
    }
}

[Collection("Sequential")]
public class GetCommentControllerTests : IClassFixture<CustomeWebApplicationFactory>
{
    private readonly CustomeWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public GetCommentControllerTests(CustomeWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }
    
    [Fact]
    public async Task GetAllComments_ReturnsSuccessStatusCode()
    {
        // Arrange
        
        // Act
        var response = await _client.GetAsync("/Comments/all");
        var comments = await response.Content.ReadFromJsonAsync<List<CommentResponseDTO>>();
        
        // Assert
        response.Content.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        comments.ShouldNotBeNull();
        comments.ShouldBeOfType(typeof(List<CommentResponseDTO>));
        comments.Count.ShouldBe(3);
    }
    
    [Fact]
    public async Task GetAllCommentsOfPost_ReturnsSuccessStatusCode()
    {
        // Arrange
        int postId = 1;
        
        // Act
        var response = await _client.GetAsync($"/Comments/ofPost/{postId}");
        var comments = await response.Content.ReadFromJsonAsync<List<CommentResponseDTO>>();
        
        // Assert
        response.Content.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        comments.ShouldNotBeNull();
        comments.ShouldBeOfType(typeof(List<CommentResponseDTO>));
        comments.Count.ShouldBe(3);
    }

    [Fact]
    public async Task GetOneComment_ReturnsSuccessStatusCode()
    {
        // Arrange
        int id = 1;
        
        // Act
        var response = await _client.GetAsync($"/Comments/One/{id}");
        var content = await response.Content.ReadAsStringAsync();
        var comment = JsonConvert.DeserializeObject<CommentResponseDTO>(content);
        
        // Assert
        response.Content.Headers.ContentType?.MediaType.ShouldBe("application/json");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        comment.ShouldNotBeNull();
        comment.ShouldBeOfType(typeof(CommentResponseDTO));
        comment.Id.ShouldBe(id);
        comment.Text.ShouldNotBeNull();
    }
}

[Collection("Sequential")]
public class UpdateCommentControllerTests : IClassFixture<CustomeWebApplicationFactory>
{
    private readonly CustomeWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public UpdateCommentControllerTests(CustomeWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }
    
    [Fact]
    public async Task UpdateComment_ReturnSuccessStatusCode()
    {
        // Arrange
        int id = 1;
        var res = await _client.GetAsync($"/Comments/One/{id}");
        var originalComment = JsonConvert.DeserializeObject<CommentResponseDTO>(await res.Content.ReadAsStringAsync());
       
        
        var updateCommentDto = new CommentRequestDTO
        {
            Text = $"Test comment {id} updated",
            UserId = 2,
            PostId = 1
        };
        
        // Act
        var putResponse = await _client.PutAsJsonAsync($"/Comments/{id}", updateCommentDto);
        
        var getResponse = await _client.GetAsync($"/Comments/One/{id}");
        var updatedComment = JsonConvert.DeserializeObject<CommentResponseDTO>(await getResponse.Content.ReadAsStringAsync());

        // Assert
        putResponse.Content.Headers.ContentType?.MediaType.ShouldBe("application/json");
        putResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        // Assert
        getResponse.Content.Headers.ContentType?.MediaType.ShouldBe("application/json");
        getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
       
        updatedComment.ShouldNotBeNull();
        updatedComment.ShouldBeOfType(typeof(CommentResponseDTO));
        updatedComment.Id.ShouldBe(id);
        updatedComment.Text.ShouldNotBeNull();
        // updatedComment.UserId.ShouldBe(originalComment.UserId);
        // updatedComment.PostId.ShouldBe(originalComment.PostId);
    }

}

[Collection("Sequential")]
public class DeleteCommentControllerTests : IClassFixture<CustomeWebApplicationFactory>
{
    private readonly CustomeWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public DeleteCommentControllerTests(CustomeWebApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }
    
    [Fact]
    public async Task DeleteComment_ReturnSuccessStatusCode()
    {
        // Arrange
        int id = 1;
        
        // Act
        var deleteResponse = await _client.DeleteAsync($"/Comments/{id}");
        var getResponse = await _client.GetAsync($"/Comments/One/{id}");
        
        // Act
        var getAllResponse = await _client.GetAsync($"/Comments/all");
        var comments = await getAllResponse.Content.ReadFromJsonAsync<List<CommentResponseDTO>>();
        
        // Assert
        deleteResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        getResponse.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
        getAllResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        comments.ShouldNotBeNull();
        comments.ShouldBeOfType(typeof(List<CommentResponseDTO>));
        comments.Count.ShouldBe(2); // since one is deleted
    }
    
}