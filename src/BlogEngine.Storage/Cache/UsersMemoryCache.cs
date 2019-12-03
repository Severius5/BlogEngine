using BlogEngine.Storage.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;

namespace BlogEngine.Storage.Cache
{
    internal class UsersMemoryCache
    {
        public IMemoryCache Cache { get; }

        public UsersMemoryCache(IOptions<UsersCachingOptions> options)
        {
            if (options?.Value == null)
                throw new ArgumentNullException(nameof(options));

            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = options.Value.MaxCachedUsers
            });
        }
    }
}
