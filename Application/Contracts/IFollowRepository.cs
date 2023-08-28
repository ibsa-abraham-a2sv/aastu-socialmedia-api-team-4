using Application.Contracts.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Contracts;

public interface IFollowRepository : IGenericRepository<FollowEntity>
{
    public Task<List<UserEntity>> GetFollowersList(int UserId);
    public Task<List<UserEntity>> GetFollowingList(UserEntity userEntity);
    public Task<bool> DeleteFollow(FollowEntity followEntity);
    public Task<bool> FollowExists(int followerId, int followingId);
}