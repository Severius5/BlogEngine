using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Storage.ProviderContexts
{
    internal class SqliteBlogContext : BlogContext
    {
        public SqliteBlogContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
