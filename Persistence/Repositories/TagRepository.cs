using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class TagRepository : GenericRepository<TagEntity>, ITagRepository
{
    private readonly AppDBContext appDBContext;
    
    public TagRepository(AppDBContext appDBContext) : base(appDBContext) 
    {
        this.appDBContext = appDBContext;
    }
    
    public async Task<List<TagEntity>> SearchTagsByName(string name)
    {
        var tags = await appDBContext.Tags.Include(tag => tag.Posts).Where(t => t.Name.Contains(name)).ToListAsync();
        return tags;
    }
    
    public async Task<TagEntity?> GetTagByName(string name)
    {
        var tag = await appDBContext.Tags.FirstOrDefaultAsync(t => t.Name == name);
        return tag;
    }
}