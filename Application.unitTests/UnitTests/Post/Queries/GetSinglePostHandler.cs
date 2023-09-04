using Application.DTOs.Post;
using Application.Features.Post.Queries.GetSinglePost;
using Application.Profiles;
using Application.unitTests.Mocks;
using AutoMapper;
using Moq;
using Shouldly; 

namespace Application.UnitTests.Post.Queries
{
    public class GetSinglePostRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPostRepository> _mockRepo;
        public GetSinglePostRequestHandlerTests()
        {
            _mockRepo = PostMockRepository.GetPostRepository();

            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetSinglePostTest()
        {
            
            var postId = 1; 
            var handler = new GetSinglePostRequestHandler(_mockRepo.Object, _mapper);

            var result = await handler.Handle(new GetSinglePostRequest { PostId = postId }, CancellationToken.None);

            Assert.IsType<PostResponseDto>(result);
            Assert.Equal(postId, result.Id);
        }
    }
}
