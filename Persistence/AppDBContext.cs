using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class AppDBContext : DbContext
    {
        public DbSet<CommentEntity> Comments { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CommentEntity>(entity =>
            {
            });
        }
    }
}
