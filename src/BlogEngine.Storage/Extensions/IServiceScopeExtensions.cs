using BlogEngine.Storage;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceScope"/>
    /// </summary>
    public static class IServiceScopeExtensions
    {
        /// <summary>
        /// Perform storage update
        /// </summary>
        /// <param name="scope"><see cref="IServiceScope"/></param>
        public static void UpdateBlogStorage(this IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetRequiredService<BlogContext>();
            context.Database.Migrate();
        }
    }
}
