using Application.Contracts;
using Application.DTOs.Comment;
using Application.Features.Comment.Commands.DeleteComment;
using Application.Features.Comment.Commands.UpdateComment;
using Application.Features.Comment.Queries.GetOneComment;
using Application.Profiles;
using Application.unitTests.CommentUnitTests.Mocks;
using AutoMapper;
using Shouldly;

namespace Application.unitTests.CommentUnitTests;

public class GetUpdateDeleteNotFoundExceptionTests
{
    private readonly IMapper _mapper;
    private readonly ICommentRepository _mockedCommentRepo;

    public GetUpdateDeleteNotFoundExceptionTests()
    {
        var config = new MapperConfiguration(c => c.AddProfile(new MappingProfile()));
        _mapper = config.CreateMapper();
        _mockedCommentRepo = MockCommentRepoAllMethods.GetMockRepo().Object;
    }

    [Fact]
    public async Task GetById_ReturnsNotFound()
    {
        // Arrange
        GetOneCommentQueryHandler getCommentByIdQueryHandler = new(_mockedCommentRepo, _mapper);
        var getCommentByIdQuery = new GetOneCommentQuery()
        {
            Id = 4,
        };
        
        // Act
        var req = async () => await getCommentByIdQueryHandler.Handle(getCommentByIdQuery, CancellationToken.None);
        
        // Assert
        await req.ShouldThrowAsync<Exception>();
        
        
        // Act
        var req2 = async () => await getCommentByIdQueryHandler.Handle(new GetOneCommentQuery(){Id = 2}, CancellationToken.None);
        
        // Assert
        await req2.ShouldNotThrowAsync();
    }
    
    [Fact]
    public async Task UpdateComment_ReturnsNotFound()
    {
        // Arrange
        UpdateCommentCommandHandler updateCommentCommandHandler = new(_mockedCommentRepo, _mapper);
        var updateCommentCommand = new UpdateCommentCommand()
        {
            Id = 4,
            UpdateCommentDto = new CommentRequestDto()
            {
                PostId = 1,
                Text = "text",
            }
        };
        
        var updateCommentCommand2 = new UpdateCommentCommand()
        {
            Id = 2,
            UpdateCommentDto = new CommentRequestDto()
            {
                PostId = 2,
                Text = "updated text",
            }
        };
        
        // Act
        var req = async () => await updateCommentCommandHandler.Handle(updateCommentCommand, CancellationToken.None);
        
        // Assert
        await req.ShouldThrowAsync<Exception>();
        
        
        // Act
        var req2 = async () => await updateCommentCommandHandler.Handle(updateCommentCommand2 ,CancellationToken.None);
        
        // Assert
        await req2.ShouldNotThrowAsync();
    }
    
    [Fact]
    public async Task DeleteComment_ReturnsNotFound()
    {
        // Arrange
        DeleteCommentCommandHandler deleteCommentCommandHandler = new(_mockedCommentRepo, _mapper);
        var deleteCommentCommand = new DeleteCommentCommand()
        {
            Id = 4,
        };
        
        // Act
        var req = async () => await deleteCommentCommandHandler.Handle(deleteCommentCommand, CancellationToken.None);
        
        // Assert
        await req.ShouldThrowAsync<Exception>();
        
        
        // Act
        var req2 = async () => await deleteCommentCommandHandler.Handle(new DeleteCommentCommand(){Id = 2}, CancellationToken.None);
        
        // Assert
        await req2.ShouldNotThrowAsync();
    }
}