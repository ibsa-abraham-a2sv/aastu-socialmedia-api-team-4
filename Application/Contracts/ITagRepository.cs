using Application.Contracts.Common;
using Application.DTOs.Tag;
using Domain.Entities;

namespace Application.Contracts;

public interface ITagRepository : IGenericRepository<TagEntity>
{
    public Task<List<TagEntity>> SearchTagsByName(string name);
    public Task<TagEntity?> GetTagByName(string name);
}