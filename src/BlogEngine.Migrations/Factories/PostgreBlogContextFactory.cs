using BlogEngine.Storage.ProviderContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlogEngine.Migrations.Factories
{
    internal class PostgreBlogContextFactory : IDesignTimeDbContextFactory<PostgreBlogContext>
    {
        public PostgreBlogContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PostgreBlogContext>();
            builder.UseNpgsql("invalid", opt => opt.MigrationsAssembly("BlogEngine.Migrations"));
            return new PostgreBlogContext(builder.Options);
        }
    }
}
