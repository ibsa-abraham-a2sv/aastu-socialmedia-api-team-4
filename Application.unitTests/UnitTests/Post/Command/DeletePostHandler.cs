using Application.Contracts;
using Application.Features.Post.Commands.DeletePost;
using Application.Profiles;
using Application.unitTests.Mocks;
using AutoMapper;
using MediatR;
using Moq;
using Shouldly;

namespace Application.UnitTests.Post.Commands
{
    public class DeletePostCommandHandlerTests
    {
        private readonly IMapper _mockMapper;
        private readonly Mock<IPostRepository> _mockPostRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        public DeletePostCommandHandlerTests()
        {
            _mockPostRepository = PostMockRepository.GetPostRepository();
            _mockUserRepository = UserMockRepository.GetUserRepository();

            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<ProfileMapping>();
            });

            _mockMapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task DeletePost()
        {

            var command = new DeletePostCommand{PostId = 2};
            var handler = new DeletePostCommandHandler(_mockPostRepository.Object, _mockMapper);

            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldBe(Unit.Value);
        }
    }
}
