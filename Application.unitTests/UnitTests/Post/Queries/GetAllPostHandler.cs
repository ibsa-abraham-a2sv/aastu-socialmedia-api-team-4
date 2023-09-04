using Application.DTOs.Post;
using Application.Features.Post.Queries.GetAllPosts;
using Application.Features.Post.Queries.GetSinglePost;
using Application.Profiles;
using Application.unitTests.Mocks;
using AutoMapper;
using Shouldly;
using Moq;

namespace Application.UnitTests.Post.Queries
{
    public class GetAllPostsRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPostRepository> _mockRepo;
        public GetAllPostsRequestHandlerTests()
        {
            _mockRepo = PostMockRepository.GetPostRepository();

            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<ProfileMapping>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetAllPostsTest()
        {
            var handler = new GetAllPostsRequestHandler(_mockRepo.Object, _mapper);

            var result = await handler.Handle(new GetAllPostsRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<PostResponseDto>>();

            result.Count.ShouldBe(3);
        }
    }
}