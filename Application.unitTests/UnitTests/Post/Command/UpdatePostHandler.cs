using Application.Contracts;
using Application.Contracts.Services;
using Application.DTOs.Post;
using Application.Features.Post.Commands.DeletePost;
using Application.Features.Post.Commands.UpdatePost;
using Application.Profiles;
using Application.unitTests.Mocks;
using AutoMapper;
using Infrastructure.FileUpload;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;

namespace Application.UnitTests.Post.Commands
{
    public class UpdatePostCommandHandlerTests
    {
        private readonly IMapper _mockMapper;
        private readonly IFileUploader _fileUploader;
        private readonly Mock<IPostRepository> _mockPostRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        public UpdatePostCommandHandlerTests()
        {
            _mockPostRepository = PostMockRepository.GetPostRepository();
            _mockUserRepository = UserMockRepository.GetUserRepository();

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
        public async Task UpdatePost()
        {
            var updatePost = new PostRequestDto{Content = "Content 2",Title = "Title 2"};
            var command = new UpdatePostCommand{PostId = 2,UserId=2,UpdatePost=updatePost};
            var handler = new UpdatePostCommandHandler(_fileUploader,_mockPostRepository.Object, _mockUserRepository.Object, _mockMapper);

            var result = await handler.Handle(command, CancellationToken.None);
            
            result.ShouldBe(Unit.Value);

            var updatedPost = await _mockPostRepository.Object.GetByIdAsync(command.PostId);
            updatedPost.ShouldNotBeNull();
            updatedPost.Content.ShouldBe(updatePost.Content);
            updatedPost.Title.ShouldBe(updatePost.Title);
            
        }
    }
}
