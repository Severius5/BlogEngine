using BlogEngine.DTO.Models;
using BlogEngine.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogEngine.Storage
{
    /// <summary>
    /// Blog context
    /// </summary>
    internal class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .SeedDatabase()
                .Entity<Post>(entity =>
                {
                    entity.HasIndex(x => x.NormalizedTitle).IsUnique();
                    entity.Property(x => x.Status).HasConversion(new EnumToNumberConverter<PostStatus, int>());
                })
                .Entity<User>(entity =>
                {
                    entity.HasIndex(x => x.NormalizedUsername).IsUnique();
                    entity.HasIndex(x => x.NormalizedEmail).IsUnique();
                });
        }
    }
}
