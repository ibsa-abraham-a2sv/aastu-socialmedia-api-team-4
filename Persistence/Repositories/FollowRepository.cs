using Application.Contracts;
using Domain.Entities;
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

    public Task<List<UserEntity>> GetFollowingList(UserEntity userEntity)
    {
        var following = _dbContext.Follow
            .Where(f => f.FollowerId == userEntity.Id)
            .Select(f => f.Following)
            .ToListAsync();

        return following;
    }
}