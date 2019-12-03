using BlogEngine.Storage.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;

namespace BlogEngine.Storage.Cache
{
    internal class PostsMemoryCache
    {
        public IMemoryCache Cache { get; }

        public PostsMemoryCache(IOptions<PostsCachingOptions> cacheOptions)
        {
            if (cacheOptions?.Value == null)
                throw new ArgumentNullException(nameof(cacheOptions));

            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = cacheOptions.Value.MaxCachedPosts
            });
        }
    }
}
