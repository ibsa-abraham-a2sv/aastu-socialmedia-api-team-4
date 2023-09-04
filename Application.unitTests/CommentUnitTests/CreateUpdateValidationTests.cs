using Application.Contracts;
using Application.DTOs.Comment;
using Application.Features.Comment.Commands.CreateComment;
using Application.Features.Comment.Commands.UpdateComment;
using Application.Profiles;
using Application.unitTests.CommentUnitTests.Mocks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Moq;
using Shouldly;

namespace Application.unitTests.CommentUnitTests;

public class CreateUpdateValidationTests
{
    private readonly IMapper _mapper;
    private readonly ICommentRepository _mockedCommentRepo;
    public CreateUpdateValidationTests()
    {
        _mapper = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); }).CreateMapper();
        _mockedCommentRepo = MockCommentRepoAllMethods.GetMockRepo().Object;
    }
    
    [Fact]
    public async Task CreateComment_ThrowsValidationException()
    {
        CreateCommentCommandHandler createCommentCommandHandler = new(_mockedCommentRepo, _mapper);
        var createCommentCommand = new CreateCommentCommand()
        {
            userId = 1,
            commentRequestDTO = new CommentRequestDto()
            {
                PostId = 1,
                Text = "",
            }
        };
        
        var createCommentCommand2 = new CreateCommentCommand()
        {
            userId = 1,
            commentRequestDTO = new CommentRequestDto()
            {
                PostId = 0,
                Text = "Comment Text",
            }
        };
        
        var createCommentCommand3 = new CreateCommentCommand()
        {
            userId = 1,
            commentRequestDTO = new CommentRequestDto()
            {
                PostId = 1,
                Text = "Comment Text",
            }
        };

        // create request, Arrange
        Func<Task<CommentResponseDTO>> req = async () =>
            await createCommentCommandHandler.Handle(createCommentCommand, CancellationToken.None);

        // Act and Assert
        await Should.ThrowAsync<ValidationException>(req);
        
        // create request,Arrange 
        req = async () => await createCommentCommandHandler.Handle(createCommentCommand2, CancellationToken.None);
        
        // Act and Assert
        await Should.ThrowAsync<ValidationException>(req);
        
        // create request, Arrange
        req = async () => await createCommentCommandHandler.Handle(createCommentCommand3, CancellationToken.None);
        
        // Act and Assert
        await Should.NotThrowAsync(req);
    }


    [Fact]
    public async Task UpdateComment_ThrowsValidationException()
    {
        // Arrange
        UpdateCommentCommandHandler updateCommentCommandHandler = new(_mockedCommentRepo, _mapper);
        var updateCommentCommand = new UpdateCommentCommand()
        {
            Id = 1,
            UpdateCommentDto = new CommentRequestDto()
            {
                PostId = 1,
                Text = "Text", // text should be either empty or more than 10 characters 
            }
        };

        var updateCommentCommand2 = new UpdateCommentCommand()
        {
            Id = 1,
            UpdateCommentDto = new CommentRequestDto()
            {
                PostId = -1, // post id should be greater than 0
                Text = "Comment Text",
            }
        };

        var updateCommentCommand3 = new UpdateCommentCommand()
        {
            Id = 1,
            UpdateCommentDto = new CommentRequestDto()
            {
                PostId = 1,
                Text = "Comment Text",
            }
        };

        // create request, Act
        Func<Task<Unit>> req = async () =>
            await updateCommentCommandHandler.Handle(updateCommentCommand, CancellationToken.None);

        // Assert
        await Should.ThrowAsync<ValidationException>(req);

        // create request, Act 
        req = async () => await updateCommentCommandHandler.Handle(updateCommentCommand2, CancellationToken.None);

        // Assert
        await Should.ThrowAsync<ValidationException>(req);

        // create request, Act
        req = async () => await updateCommentCommandHandler.Handle(updateCommentCommand3, CancellationToken.None);

        // Assert
        await Should.NotThrowAsync(req);
    }
}