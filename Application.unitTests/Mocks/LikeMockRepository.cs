using Application.Contracts;
using Domain.Entities;
using Moq;

namespace Application.unitTests.Mocks;

public static class LikeMockRepository
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
}