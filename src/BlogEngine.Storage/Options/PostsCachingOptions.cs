using System;

namespace BlogEngine.Storage.Options
{
    internal class PostsCachingOptions
    {
        public int MaxCachedPosts { get; set; } = 50;
        public TimeSpan PostAbsoluteExpiration { get; set; } = TimeSpan.FromMinutes(20);
        public TimeSpan PostSlidingExpiration { get; set; } = TimeSpan.FromMinutes(5);
    }
}
