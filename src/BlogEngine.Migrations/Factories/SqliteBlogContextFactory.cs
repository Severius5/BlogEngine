using BlogEngine.Storage.ProviderContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlogEngine.Migrations.Factories
{
    internal class SqliteBlogContextFactory : IDesignTimeDbContextFactory<SqliteBlogContext>
    {
        public SqliteBlogContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SqliteBlogContext>();
            builder.UseSqlite("invalid", opt => opt.MigrationsAssembly("BlogEngine.Migrations"));
            return new SqliteBlogContext(builder.Options);
        }
    }
}
