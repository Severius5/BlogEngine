using BlogEngine.Storage;
using BlogEngine.Storage.Cache;
using BlogEngine.Storage.Options;
using BlogEngine.Storage.ProviderContexts;
using BlogEngine.Storage.Repositories;
using BlogEngine.Storage.Repositories.Internals;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogStorage(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddRepositoryCache(configuration.GetSection("RepositoryCache"))
                .AddBlogContext(configuration)
                .AddBlogRepositories();
        }

        private static IServiceCollection AddRepositoryCache(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<PostsCachingOptions>(configuration.GetSection("Posts"))
                .Configure<UsersCachingOptions>(configuration.GetSection("Authors"))
                .AddSingleton<PostsMemoryCache>()
                .AddSingleton<UsersMemoryCache>();
        }

        private static IServiceCollection AddBlogRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient<IPostRepository, PostRepository>()
                .Decorate<IPostRepository, PostCacheRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                .Decorate<IUserRepository, UserCacheRepository>();
        }

        private static IServiceCollection AddBlogContext(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.Get<StorageOptions>();
            switch (options.Provider)
            {
                case DbProvider.Sqlite:
                    return services.AddDbContext<BlogContext, SqliteBlogContext>(builder => builder.UseSqlite(
                         options.ConnectionString,
                         opt => opt.MigrationsAssembly("BlogEngine.Migrations")));

                case DbProvider.PostgreSQL:
                    return services.AddDbContext<BlogContext, PostgreBlogContext>(builder => builder.UseNpgsql(
                         options.ConnectionString,
                         opt => opt.MigrationsAssembly("BlogEngine.Migrations")));

                default:
                    throw new NotSupportedException($"Provider {options.Provider} is not supported");
            }
        }
    }
}
