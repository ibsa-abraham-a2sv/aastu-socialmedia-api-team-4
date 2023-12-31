using Application.Contracts;
using Application.Contracts.Services;
using Application.DTOs.Post;
using Application.Features.Post.Commands.CreatePost;
using Application.Profiles;
using Application.unitTests.Mocks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using Infrastructure.FileUpload;
using Microsoft.Extensions.Configuration;

namespace Application.UnitTests.Post.Commands
{
    public class CreatePostCommandHandlerTests
    {

        private readonly IMapper _mockMapper;
        private readonly Mock<IPostRepository> _mockPostRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ITagRepository> _mockTagRepository;
        private readonly IFileUploader _fileUploader;
        public CreatePostCommandHandlerTests()
        {
            _mockPostRepository = PostMockRepository.GetPostRepository();
            _mockUserRepository = UserMockRepository.GetUserRepository();
            _mockTagRepository = TagMockRepository.GetTagRepository();

            var con = new Dictionary<string, string>
            {
                { "Cloudinary:cloudinaryUrl", "cloudinary://593376442735824:X2DSGSO2bV9PDjqpOTb7HVYLGgQ@dkphkzco3" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(con)
                .Build();

            var serviceProvider = new ServiceCollection()
                .Configure<CloudinaryUrl>(configuration.GetSection("Cloudinary"))
                .AddScoped<IFileUploader, FileUploader>()
                .BuildServiceProvider();

            _fileUploader = serviceProvider.GetRequiredService<IFileUploader>();


            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });

            _mockMapper = mapperConfig.CreateMapper();

        }

        [Fact]
        public async Task CreatePost()
        {

            var createdPost = new PostRequestDto
            {
                Content = "Content 4",
                Title = "Title 4",
                PictureFile = null
            };

            var command = new CreatePostCommand
            {
                UserId = 1,
                NewPost = createdPost
            };

            var handler = new CreatePostCommandHandler(_fileUploader,_mockPostRepository.Object, _mockUserRepository.Object, _mockTagRepository.Object, _mockMapper);
            var result = await handler.Handle(command,CancellationToken.None);

            result.ShouldBeOfType<PostResponseDto>();
            result.Id.ShouldBe(4);
        }
    }
}
