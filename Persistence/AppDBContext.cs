using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class AppDBContext : DbContext
    {
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<PostEntity> Posts { get; set;}
        public DbSet<LikeEntity> Like { get; set; }
        public DbSet<FollowEntity> Follow { get; set; }
        public DbSet<NotificationEntity> Notification { get; set; }
        public DbSet<UserConnectionEntity> UserConnectionMappings { get; set; }
        
        public DbSet<TagEntity> Tags { get; set; }
        
        public DbSet<UserEntity> Users { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<CommentEntity>(entity =>
            // {
            //     entity.HasKey(e => e.Id);
            //
            //     entity.HasOne(c => c.User)
            //         .WithMany(u => u.Comments)
            //         .HasForeignKey(c => c.UserId)
            //         .OnDelete(DeleteBehavior.Cascade);
            //     entity.HasOne(c => c.Post)
            //         .WithMany(p => p.Comments)
            //         .HasForeignKey(c => c.PostId)
            //         .OnDelete(DeleteBehavior.Cascade);
            // });

        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(entry => entry.State == EntityState.Modified)
                .Select(entry => entry.Entity);

            foreach (var entity in entities)
            {
                var updatedAtProperty = entity.GetType().GetProperty("ModifiedAt");
                if (updatedAtProperty != null && updatedAtProperty.PropertyType == typeof(DateTime))
                {
                    updatedAtProperty.SetValue(entity, DateTime.UtcNow);
                }
            }
        }
    }
}
