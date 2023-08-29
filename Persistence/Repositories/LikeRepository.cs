using Application.Contracts;
using Application.Exceptions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.Repositories;

public class LikeRepository : GenericRepository<LikeEntity>, ILikeRepository
{
    private readonly AppDBContext _dbContext;

    public LikeRepository(AppDBContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LikeEntity> CreateLike(LikeEntity likeEntity)
    {
        // check if the like already exists
        var likeExists =
            await _dbContext.Likes.FirstOrDefaultAsync(l =>
                l.UserId == likeEntity.UserId && l.PostId == likeEntity.PostId);

        if (likeEntity != null)
            return likeExists;
        
        var like = await _dbContext.Likes.AddAsync(likeEntity);
        await _dbContext.SaveChangesAsync();

        var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == likeEntity.PostId);

        post.LikeCount = post.LikeCount + 1;

        await _dbContext.SaveChangesAsync();
        
        

        return like.Entity;
    }

    public async Task<bool> DeleteLikeByPostId(int postId, int userId)
    {
        var like = await _dbContext.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);

        if (like == null)
            throw new NotFoundException($"{postId} post not found", postId); 

        _dbContext.Likes.Remove(like);
        await _dbContext.SaveChangesAsync();
        
        var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);

        post.LikeCount = post.LikeCount - 1;

        await _dbContext.SaveChangesAsync();

        return true;
    }
}