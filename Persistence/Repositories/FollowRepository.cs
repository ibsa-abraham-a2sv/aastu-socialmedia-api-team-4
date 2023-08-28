using Application.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class FollowRepository : GenericRepository<FollowEntity>, IFollowRepository
{
    private readonly AppDBContext _dbContext;

    public FollowRepository(AppDBContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<UserEntity>> GetFollowersList(int UserId)
    {
        var followers = _dbContext.Follow.Where(f => f.FollowingId == UserId)
            .Select(f => f.Follower)
            .ToListAsync();

        return followers;
    }

    public async Task<List<UserEntity>> GetFollowingList(int UserId)
    {
        var following = await _dbContext.Follow.Where(f => f.FollowerId == UserId)
            .Select(f => f.Following)
            .ToListAsync();

        return following;
    }

    public async Task<bool> DeleteFollow(FollowEntity followEntity)
    {
        var follow = await _dbContext.Follow.FirstOrDefaultAsync(
            f => f.FollowerId == followEntity.FollowerId && f.FollowingId == followEntity.FollowingId
        );

        if (follow == null)
            return false;

        _dbContext.Follow.Remove(follow);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> FollowExists(int followerId, int followingId)
    {
        var follow = await _dbContext.Follow.FirstOrDefaultAsync(
            f => f.FollowerId == followerId && f.FollowingId == followingId
        );

        return follow != null;
    }
}