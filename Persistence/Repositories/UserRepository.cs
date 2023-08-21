using Application.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    { 
        private readonly AppDBContext _dbContext;

        public UserRepository(AppDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
