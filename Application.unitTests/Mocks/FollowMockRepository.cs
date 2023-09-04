using Application.Contracts;
using Domain.Entities;
using Moq;

namespace Application.unitTests.Mocks;

public static class FollowMockRepository
{
    public static Mock<IFollowRepository> GetFollowRepository()
    {
        var follows = new List<FollowEntity>()
        {
            new FollowEntity()
            {
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                FollowerId = 3,
                FollowingId = 2
            },
            new FollowEntity()
            {
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                FollowerId = 1,
                FollowingId = 2
            },
            new FollowEntity()
            {
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                FollowerId = 2,
                FollowingId = 3
            }
            
        };

        var mockFollowRepository = new Mock<IFollowRepository>();

        mockFollowRepository.Setup(f => f.CreateAsync(It.IsAny<FollowEntity>())).ReturnsAsync((FollowEntity follow) =>
        {
            follows.Add(follow);
            return follow;
        });

        return mockFollowRepository;
    }
}