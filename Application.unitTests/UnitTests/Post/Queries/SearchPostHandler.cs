using Application.DTOs.Post;
using Application.Features.Post.Queries.GetAllPosts;
using Application.Features.Post.Queries.GetSinglePost;
using Application.Profiles;
using Application.unitTests.Mocks;
using AutoMapper;
using Shouldly;
using Moq;
using Application.Features.Post.Queries.SearchPost;

namespace Application.UnitTests.Post.Queries
{
    public class SearchPostRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPostRepository> _mockRepo;
        public SearchPostRequestHandlerTests()
        {
            _mockRepo = PostMockRepository.GetPostRepository();

            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<ProfileMapping>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task SearchPostTest()
        {
            var query = "Content"; 
            var handler = new SearchPostRequestHandler(_mockRepo.Object, _mapper);

            var result = await handler.Handle(new SearchPostRequest { Query = query }, CancellationToken.None);

            result.ShouldBeOfType<List<PostResponseDto>>();

            // result.Count.ShouldBe(3);
        }
    }
}