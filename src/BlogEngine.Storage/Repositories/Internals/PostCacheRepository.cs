using BlogEngine.DTO.Models;
using BlogEngine.Storage.Cache;
using BlogEngine.Storage.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Storage.Repositories.Internals
{
    internal class PostCacheRepository : IPostRepository
    {
        private readonly IPostRepository _postRepository;
        private readonly IMemoryCache _cache;
        private readonly PostsCachingOptions _cacheOptions;

        public PostCacheRepository(IPostRepository postRepository, PostsMemoryCache cache, IOptionsSnapshot<PostsCachingOptions> cacheOptions)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _cache = cache.Cache ?? throw new ArgumentNullException(nameof(cache));
            _cacheOptions = cacheOptions?.Value ?? throw new ArgumentNullException(nameof(cacheOptions));
        }

        public Task ChangePostStatus(int id, PostStatus newStatus)
        {
            _cache.Remove(id);

            return _postRepository.ChangePostStatus(id, newStatus);
        }

        public async Task<int> CreatePost(BlogPost post)
        {
            var id = await _postRepository.CreatePost(post);

            _cache.Remove(id);

            return id;
        }

        public Task EditPost(BlogPost post)
        {
            _cache.Remove(post.Id);

            return _postRepository.EditPost(post);
        }

        public async Task<BlogPost> GetPost(int id)
        {
            if (!_cache.TryGetValue(id, out BlogPost post))
            {
                post = await _postRepository.GetPost(id);
                if (post == null)
                    return null;

                using (var cacheEntry = _cache.CreateEntry(id))
                {
                    cacheEntry.SetAbsoluteExpiration(_cacheOptions.PostAbsoluteExpiration);
                    cacheEntry.SetSlidingExpiration(_cacheOptions.PostSlidingExpiration);
                    cacheEntry.SetSize(1);
                    cacheEntry.SetValue(post);
                }
            }

            return post;
        }

        public Task<(IList<BlogPost> posts, int totalPosts)> GetPosts(PostsFilter filter)
        {
            return _postRepository.GetPosts(filter);
        }

        public Task DeletePost(int id)
        {
            _cache.Remove(id);

            return _postRepository.DeletePost(id);
        }
    }
}
