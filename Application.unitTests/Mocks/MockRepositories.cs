using Application.Contracts;
using Domain.Entities;
using Moq;

namespace Application.unitTests.Mocks;

public static class MockRepositories
{
    public static Mock<ILikeRepository> GetLikeRepository()
    {
        var likes = new List<LikeEntity>
        {
            new LikeEntity
            {
                Id = 1,
                UserId = 1,
                PostId = 1
            },
            new LikeEntity
            {
                Id = 2,
                UserId = 2,
                PostId = 1
            },
            new LikeEntity
            {
                Id = 3,
                UserId = 3,
                PostId = 1
            }
        };
        
        var mockLikeRepository = new Mock<ILikeRepository>();

        mockLikeRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(likes);

        return mockLikeRepository;
    }
    
    public static Mock<IPostRepository> GetPostRepository()
    {
        var posts = new List<PostEntity>
        {
            new PostEntity
            {
                Id = 1,
                UserId = 1,
                Content = "Content 1",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new PostEntity
            {
                Id = 2,
                UserId = 2,
                Content = "Content 2",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new PostEntity
            {
                Id = 3,
                UserId = 3,
                Content = "Content 3",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            }
        };
        
        var mockPostRepository = new Mock<IPostRepository>();

        mockPostRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(posts);

        return mockPostRepository;
    }
}