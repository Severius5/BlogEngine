using BlogEngine.DTO.Models;
using BlogEngine.Storage.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Storage.Repositories.Internals
{
    internal class UserCacheRepository : IUserRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _cache;
        private readonly UsersCachingOptions _cacheOptions;

        public UserCacheRepository(IUserRepository userRepository, IMemoryCache cache, IOptionsSnapshot<UsersCachingOptions> cacheOptions)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _cacheOptions = cacheOptions?.Value ?? throw new ArgumentNullException(nameof(cacheOptions));
        }

        public Task BlockUser(int userId)
        {
            _cache.Remove(userId);

            return _userRepository.BlockUser(userId);
        }

        public Task ChangeUserAdminStatus(int userId, bool isAdmin)
        {
            _cache.Remove(userId);

            return _userRepository.ChangeUserAdminStatus(userId, isAdmin);
        }

        public Task EditUser(BlogUser user)
        {
            _cache.Remove(user.Id);

            return _userRepository.EditUser(user);
        }

        public Task<BlogUser> GetUser(string email)
        {
            return _userRepository.GetUser(email); //todo: ogarnąć
        }

        public async Task<BlogUser> GetUser(int id)
        {
            if(!_cache.TryGetValue(id, out BlogUser user))
            {
                user = await _userRepository.GetUser(id);
                if (user == null)
                    return null;

                using(var cacheEntry = _cache.CreateEntry(id))
                {
                    cacheEntry.SetAbsoluteExpiration(_cacheOptions.UserAbsoluteExpiration);
                    cacheEntry.SetSize(1);
                    cacheEntry.SetValue(user);
                }
            }

            return user;
        }

        public Task<(IList<BlogUser> users, int totalUsers)> GetUsers(UsersFilter filter)
        {
            return _userRepository.GetUsers(filter);
        }

        public Task UnblockUser(int userId)
        {
            _cache.Remove(userId);

            return _userRepository.UnblockUser(userId);
        }
    }
}
