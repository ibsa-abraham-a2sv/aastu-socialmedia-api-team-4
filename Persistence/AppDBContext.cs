using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace Persistence
{
    public class AppDBContext : DbContext
    {
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<PostEntity> Posts { get; set;}
        public DbSet<LikeEntity> Likes { get; set; }
        public DbSet<FollowEntity> Follows { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<UserConnectionEntity> UserConnectionMappings { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<CommentEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                // comment - user relationship (many to one)
                entity.HasOne(c => c.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                // comment - post relationship (many to one)
                entity.HasOne(c => c.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(c => c.PostId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // post - user relationship (many to one)
            modelBuilder.Entity<PostEntity>().HasOne<UserEntity>(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // post - user relationship (many to many)
            modelBuilder.Entity<PostEntity>().HasMany<UserEntity>(p => p.Likers)
                .WithMany(u => u.LikedPosts)
                .UsingEntity<LikeEntity>(
                    j => j.ToTable("Likes")
                );

            // user - notification relationship (one to many)
            modelBuilder.Entity<UserEntity>(entity =>
                entity.HasMany(u => u.Notifications)
                    .WithOne(n => n.User)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
            );
            
            // user - user relationship (many to many => follow)
            modelBuilder.Entity<UserEntity>(entity =>
                entity.HasMany(u => u.Followers)
                    .WithMany(u => u.Following)
                    .UsingEntity<FollowEntity>(
                        j => j.ToTable("Follows").HasOne(f => f.Following).WithMany().HasForeignKey(f => f.FollowingId).OnDelete(DeleteBehavior.Cascade),
                        j => j.ToTable("Follows").HasOne(f => f.Follower).WithMany().HasForeignKey(f => f.FollowerId).OnDelete(DeleteBehavior.Cascade)
                    )
            );
            
            // user - connection relationship (one to one)
            modelBuilder.Entity<UserEntity>(entity =>
                entity.HasOne(u => u.ConnectionString)
                    .WithOne(c => c.UserEntity)
                    .HasForeignKey<UserConnectionEntity>(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
            );
            
            // post - tag relationship (many to many)
            modelBuilder.Entity<PostEntity>(entity =>
                entity.HasMany(p => p.Tags)
                    .WithMany(t => t.Posts)
                    .UsingEntity<PostTag>(
                        j => j.ToTable("PostTags")
                        .HasOne(t => t.Tag).WithMany().HasForeignKey(t => t.TagId).OnDelete(DeleteBehavior.Cascade),
                        j => j.ToTable("PostTags").HasOne(t => t.Post).WithMany().HasForeignKey(t => t.PostId).OnDelete(DeleteBehavior.Cascade)
                    )
            );
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
