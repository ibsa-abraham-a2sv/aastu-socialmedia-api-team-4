using Application.Contracts;
using Domain.Entities;
using Moq;

namespace Application.unitTests.Mocks;

public static class TagMockRepository
{
    public static Mock<ITagRepository> GetTagRepository()
    {
        var tags = new List<TagEntity>
        {
            new TagEntity
            {
                
            }
        };
        
        var mockTagRepository = new Mock<ITagRepository>();

        mockTagRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(tags);
        mockTagRepository.Setup(r => r.CreateAsync(It.IsAny<TagEntity>())).ReturnsAsync((TagEntity tag) => {
            tags.Add(tag);
            return tag;
        });

        return mockTagRepository;
    }
}