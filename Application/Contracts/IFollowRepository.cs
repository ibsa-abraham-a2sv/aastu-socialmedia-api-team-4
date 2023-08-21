using Application.Contracts.Common;
using Domain.Entities;

namespace Application.Contracts;

public interface IFollowRepository : IGenericRepository<FollowEntity>
{
    public Task<List<UserEntity>> GetFollowersList(UserEntity userEntity);
    public Task<List<UserEntity>> GetFollowingList(UserEntity userEntity);
}