using Application.Contracts;
using Application.DTOs.Follow;
using Application.DTOs.Like;
using Application.Features.Follow.Commands.CreateFollow;
using Application.Features.Like.Commands.Create_Like;
using Application.Profiles;
using Application.unitTests.Mocks;
using AutoMapper;
using Moq;

namespace Application.unitTests.UnitTests.Follow.Command;

public class CreateFollowHandlerTest
{
    
    private readonly Mock<ILikeRepository> _mockLikeRepository;
    private readonly IMapper _mapper;
    private readonly Mock<IPostRepository> _mockPostRepository;
    private readonly Mock<IFollowRepository> _mockFollowRepository;
    private readonly Mock<IUserRepository> _mockUserRepository;
    
    public CreateFollowHandlerTest()
    {
        _mockLikeRepository = LikeMockRepository.GetLikeRepository();
        _mockPostRepository = PostMockRepository.GetPostRepository();
        _mockFollowRepository = FollowMockRepository.GetFollowRepository();
        _mockUserRepository = UserMockRepository.GetUserRepository();

        var mapperConfig = new MapperConfiguration(m =>
        {
            m.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task CreateFollowHandlerTest_Success()
    {
        var handler = new CreateFollowCommandHandler(_mockFollowRepository.Object, _mockUserRepository.Object, _mapper);

        var newFollow = new FollowDto
        {
            CreatedAt = DateTime.UtcNow,
            FollowingId = 3,
            FollowerId = 2,
            ModifiedAt = DateTime.UtcNow
        };
        
        var result = await handler.Handle(new CreateFollowCommand
        {
            FollowDto = newFollow
        }, CancellationToken.None);
        
        Assert.Equal(result.FollowerId, newFollow.FollowerId);
        Assert.Equal(result.FollowingId, newFollow.FollowingId);
    }
}