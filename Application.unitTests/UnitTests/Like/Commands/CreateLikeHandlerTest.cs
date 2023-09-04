using Application.Contracts;
using Application.DTOs.Like;
using Application.Features.Like.Commands.Create_Like;
using Application.Profiles;
using Application.unitTests.Mocks;
using AutoMapper;
using Moq;
using Xunit.Abstractions;

namespace Application.unitTests.UnitTests.Like.Commands;

public class CreateLikeHandlerTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Mock<ILikeRepository> _mockLikeRepository;
    private readonly IMapper _mapper;
    private readonly Mock<IPostRepository> _mockPostRepository;

    public CreateLikeHandlerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _mockLikeRepository = LikeMockRepository.GetLikeRepository();
        _mockPostRepository = PostMockRepository.GetPostRepository();

        var mapperConfig = new MapperConfiguration(m =>
        {
            m.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }
    
    [Fact]
    public async Task CreateLikeHandler_Success()
    {
        var handler = new CreateLikeCommandHandler(_mockLikeRepository.Object, _mockPostRepository.Object, _mapper);

        var newLike = new LikeDto()
        {
            PostId = 1,
            UserId = 3
        };

        var result = await handler.Handle(new CreateLikeCommand
        {
            LikeDto = newLike
        }, CancellationToken.None);

        Assert.Equal(result.UserId, newLike.UserId);
        Assert.Equal(result.PostId, newLike.PostId);
    }
}