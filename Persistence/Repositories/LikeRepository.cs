using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Persistence.Repositories;

public class LikeRepository : GenericRepository<LikeEntity>
{
    private readonly AppDBContext _dbContext;

    public LikeRepository(AppDBContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}