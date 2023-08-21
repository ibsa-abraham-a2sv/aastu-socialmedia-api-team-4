using Application.Contracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Contracts;

public interface ILikeRepository : IGenericRepository<LikeEntity>
{
    
}