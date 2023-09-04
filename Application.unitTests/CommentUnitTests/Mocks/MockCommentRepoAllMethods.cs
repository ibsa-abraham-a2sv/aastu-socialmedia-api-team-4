using Application.Contracts;
using Application.Features.Comment.Queries.GetAllCommets;
using Domain.Entities;
using MediatR;
using Moq;

namespace Application.unitTests.CommentUnitTests.Mocks;

public static class MockCommentRepoAllMethods
{
    public static Mock<ICommentRepository> GetMockRepo()
    {
        var comments = new List<CommentEntity>
        {
            new CommentEntity()
            {
                Id = 1,
                PostId = 1,
                Text = "text",
                UserId = 1
            },
            new CommentEntity()
            {
                Id = 2,
                PostId = 2,
                Text = "text",
                UserId = 2
            },
            new CommentEntity()
            {   
                Id = 3,
                PostId = 3,
                Text = "text",
                UserId = 3
            }
        };
        var mockedCommentRepo = new Mock<ICommentRepository>();
        mockedCommentRepo.Setup(c => c.GetAllAsync()).ReturnsAsync(() => comments);
        
        // mockedCommentRepo.Setup(c => c.GetByIdAsync).ReturnsAsync(() => )
        mockedCommentRepo.Setup(c => c.GetByIdAsync(It.IsAny<int>()))!.ReturnsAsync((int id) => comments.FirstOrDefault(c => c.Id == id));
        
        mockedCommentRepo.Setup(c => c.CreateAsync(It.IsAny<CommentEntity>()))!.ReturnsAsync((CommentEntity comment) =>
        {
            comment.Id = comments.Count + 1;
            comments.Add(comment);
            return comment;
        });
        
        mockedCommentRepo.Setup(c => c.UpdateAsync(It.IsAny<int>(), It.IsAny<CommentEntity>()))!.ReturnsAsync((int id, CommentEntity comment) => Unit.Value);
        
        mockedCommentRepo.Setup(c => c.DeleteAsync(It.IsAny<int>()))!.ReturnsAsync((int id) => Unit.Value);

        mockedCommentRepo.Setup(c => c.GetCommentByPostId(It.IsAny<int>())).ReturnsAsync(() => comments.ToList());
        
        return mockedCommentRepo;
    }
}