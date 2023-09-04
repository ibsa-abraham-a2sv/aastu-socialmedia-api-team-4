using Application.Contracts;
using Application.DTOs.Comment;
using Application.Features.Comment.Commands.CreateComment;
using Application.Features.Comment.Commands.DeleteComment;
using Application.Features.Comment.Commands.UpdateComment;
using Application.Features.Comment.Queries.GetAllCommets.GetAllCommetsByPostId;
using Application.Features.Comment.Queries.GetOneComment;
using Application.Profiles;
using Application.unitTests.CommentUnitTests.Mocks;
using AutoMapper;
using MediatR;
using Moq;
using Shouldly;

namespace Application.unitTests.CommentUnitTests;

public class CommentCrudTests
{
    private readonly Mock<ICommentRepository> _mockCommentRepository;
    private readonly IMapper _mapper;

    public CommentCrudTests()
    {
        _mockCommentRepository = MockCommentRepoAllMethods.GetMockRepo();
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task CreateComment_ReturnSuccess()
    {
        // Arrange
        CreateCommentCommandHandler createCommentCommandHandler = new(_mockCommentRepository.Object, _mapper);
        var createCommentCommand = new CreateCommentCommand()
        {
           userId = 1,
           commentRequestDTO = new CommentRequestDto()
            {
                PostId = 1,
                Text = "text",
            }
        };
        
        // Act
        var allComments = await _mockCommentRepository.Object.GetAllAsync();
        allComments.Count.ShouldBe(3);
        
        var res = await createCommentCommandHandler.Handle(createCommentCommand, CancellationToken.None);
        
        // Assert
        res.Id.ShouldBe(4);
        res.Text.ShouldBe(createCommentCommand.commentRequestDTO.Text);
        var all = await _mockCommentRepository.Object.GetAllAsync();
        all.Count.ShouldBe(4);
    }
    
    // get by id
    [Fact]
    public async Task GetById_ReturnSuccess()
    {
        GetOneCommentQueryHandler getCommentByIdQueryHandler = new(_mockCommentRepository.Object, _mapper);
        var getCommentByIdQuery = new GetOneCommentQuery()
        {
            Id = 1,
        };
        
        var res = await getCommentByIdQueryHandler.Handle(getCommentByIdQuery, CancellationToken.None);
        res.Id.ShouldBe(1);
        res.Text.ShouldBe("text");
        res.UserId.ShouldBe(1);
    }
    
    // get comment by post id
    [Fact]
    public async Task GetCommentByPostId_ReturnSuccess()
    {
        GetAllCommetsByPostIdQueryHandler getCommentByPostIdQueryHandler = new(_mockCommentRepository.Object, _mapper);
        var getCommentByPostIdQuery = new GetAllCommentsByPostIdQuery()
        {
            PostId = 1,
        };
        
        var res = await getCommentByPostIdQueryHandler.Handle(getCommentByPostIdQuery, CancellationToken.None);
        res.Count.ShouldBe(3);
        res.ShouldBeOfType<List<CommentResponseDTO>>();
    }
    
    [Fact]
    public async Task UpdateComment_ReturnSuccess()
    {
        UpdateCommentCommandHandler updateCommentCommandHandler = new(_mockCommentRepository.Object, _mapper);
        var updateCommentCommand = new UpdateCommentCommand()
        {
            Id = 1,
            UpdateCommentDto = new CommentRequestDto()
            {
                PostId = 1,
                Text = "comment updated text",
            }
        };
        
        var res = await updateCommentCommandHandler.Handle(updateCommentCommand, CancellationToken.None);
        res.ShouldBe(Unit.Value);
    }
    
    [Fact]
    public async Task DeleteComment_ReturnSuccess()
    {
        DeleteCommentCommandHandler deleteCommentCommandHandler = new(_mockCommentRepository.Object, _mapper);
        var deleteCommentCommand = new DeleteCommentCommand()
        {
            Id = 1,
        };
        
        var res = await deleteCommentCommandHandler.Handle(deleteCommentCommand, CancellationToken.None);
        res.ShouldBe(Unit.Value);
    }
    
}