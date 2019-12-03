using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Storage.ProviderContexts
{
    internal class PostgreBlogContext : BlogContext
    {
        public PostgreBlogContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
