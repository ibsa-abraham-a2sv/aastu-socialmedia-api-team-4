using Application.Contracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Contracts;

public interface ILikeRepository : IGenericRepository<LikeEntity>
{
    public Task<LikeEntity> CreateLike(LikeEntity likeEntity);
    public Task<bool> DeleteLikeByPostId(int PostId);
}