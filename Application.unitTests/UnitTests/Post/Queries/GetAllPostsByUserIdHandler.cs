using Application.Contracts;
using Application.DTOs.Post;
using Application.Features.Post.Queries.GetAllPostsByUserId;
using Application.Profiles;
using Application.unitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly;

namespace Application.UnitTests.Post.Queries
{
    public class GetAllPostsByUserIdCommandHandlerTests
    {
        private readonly IMapper _mockMapper;
        private readonly Mock<IPostRepository> _mockPostRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        public GetAllPostsByUserIdCommandHandlerTests()
        {
            _mockPostRepository = PostMockRepository.GetPostRepository();
            _mockUserRepository = UserMockRepository.GetUserRepository();

            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mockMapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetPostsByUserId()
        {
            
            var handler = new GetAllPostsByUserIdRequestHandler(_mockPostRepository.Object, _mockUserRepository.Object, _mockMapper);

            var result = await handler.Handle(new GetAllPostsByUserIdRequest{UserId=2}, CancellationToken.None);
            
            result.ShouldBeOfType<List<PostResponseDto>>();
            result.Count.ShouldBe(1);
        }
    }
}
