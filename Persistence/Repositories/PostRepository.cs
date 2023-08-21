using Domain.Entities;
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
    }
}
