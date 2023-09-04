using Application.DTOs.Post;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class PostRepository : GenericRepository<PostEntity>, IPostRepository
    {
        private readonly AppDBContext _dbContext;

        public PostRepository(AppDBContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<PostEntity>> GetAllPostsByUserIdAsync(int userId)
        {
            return await _dbContext.Posts.Where(post => post.UserId == userId).ToListAsync();
        }

        public async Task<IReadOnlyList<PostEntity>> SearchPostAsync(string query)
        {
            return await _dbContext.Posts
                    .Where(p => p.Content.Contains(query) || p.Title.Contains(query))
                    .ToListAsync();
        }
        
        
    }
}
