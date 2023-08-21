using Domain.Entities;
using Application.Contracts.Common;

public interface IPostRepository : IGenericRepository<PostEntity>
{
    public Task<IReadOnlyList<PostEntity>> GetAllUserPostsAsync(int UserId);
    public Task<IReadOnlyList<PostEntity>> SearchPostAsync(string query);
}