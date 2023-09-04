using Domain.Entities;
using MediatR;
using Moq;

namespace Application.unitTests.Mocks;

public static class PostMockRepository
{
    public static Mock<IPostRepository> GetPostRepository()
    {
        var posts = new List<PostEntity>
        {
            new PostEntity
            {
                Id = 1,
                UserId = 1,
                Title = "Title 1",
                Content = "Content 1",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new PostEntity
            {
                Id = 2,
                UserId = 2,
                Title = "Title 2",
                Content = "Content 2",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new PostEntity
            {
                Id = 3,
                UserId = 3,
                Title = "Title 3",
                Content = "Content 3",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            }
        };
        
        var mockPostRepository = new Mock<IPostRepository>();

        mockPostRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(posts);
        mockPostRepository.Setup(p => p.Exists(It.IsAny<int>())).ReturnsAsync((int postId) =>
        {
            return posts.Exists(p => p.Id == postId);
        });

        
        mockPostRepository.Setup(c => c.GetByIdAsync(It.IsAny<int>()))!.ReturnsAsync((int id) => posts.FirstOrDefault(c => c.Id == id));
        
        mockPostRepository.Setup(c => c.CreateAsync(It.IsAny<PostEntity>()))!.ReturnsAsync((PostEntity post) =>
        {
            post.Id = posts.Count + 1;
            posts.Add(post);
            return post;
        });
        
        mockPostRepository.Setup(c => c.UpdateAsync(It.IsAny<int>(), It.IsAny<PostEntity>()))!.ReturnsAsync((int id, PostEntity post) => Unit.Value);
        
        mockPostRepository.Setup(c => c.DeleteAsync(It.IsAny<int>()))!.ReturnsAsync((int id) => Unit.Value);

        mockPostRepository.Setup(c => c.GetAllPostsByUserIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => posts.Where(p => p.UserId ==id).ToList());
        mockPostRepository.Setup(c => c.SearchPostAsync(It.IsAny<string>())).ReturnsAsync(() => posts.ToList());
        return mockPostRepository;
    }
}