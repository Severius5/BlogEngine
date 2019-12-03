using System;

namespace BlogEngine.Storage.Options
{
    internal class UsersCachingOptions
    {
        public int MaxCachedUsers { get; set; } = 20;
        public TimeSpan UserAbsoluteExpiration { get; set; } = TimeSpan.FromHours(5);
    }
}
