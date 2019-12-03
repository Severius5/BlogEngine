using BlogEngine.Core.Results;
using BlogEngine.DTO;
using BlogEngine.DTO.Models;
using BlogEngine.Storage.Repositories;
using System;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services.Internals
{
    internal class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public Task<BlogUser> GetUser(int userId)
        {
            return _userRepository.GetUser(userId);
        }

        public async Task<PagedResult<BlogUser>> GetUsers(UsersFilter filter)
        {
            var (users, totalUsers) = await _userRepository.GetUsers(filter);

            return new PagedResult<BlogUser>(users, totalUsers);
        }

        public async Task<ErrorResult> ChangeUserAdminStatus(int userId, bool isAdmin)
        {
            var existingUser = _userRepository.GetUser(userId);
            if (existingUser == null)
                return new ErrorResult(ErrorCodes.UserNotFound);

            await _userRepository.ChangeUserAdminStatus(userId, isAdmin);

            return new ErrorResult();
        }

        public async Task<EditUserResult> EditUser(int userId, string username, string bio)
        {
            var existingUser = await _userRepository.GetUser(userId);
            if (existingUser == null)
                return new EditUserResult(ErrorCodes.UserNotFound);

            var slug = username.ToSlug();

            existingUser.Bio = bio;
            existingUser.Slug = slug;
            existingUser.Username = username;

            await _userRepository.EditUser(existingUser);

            return new EditUserResult(userId, slug);
        }

        public async Task<ErrorResult> BlockUser(int userId)
        {
            var existingUser = await _userRepository.GetUser(userId);
            if (existingUser == null)
                return new ErrorResult(ErrorCodes.UserNotFound);

            await _userRepository.BlockUser(userId);

            return new ErrorResult();
        }

        public async Task<ErrorResult> UnblockUser(int userId)
        {
            var existingUser = await _userRepository.GetUser(userId);
            if (existingUser == null)
                return new ErrorResult(ErrorCodes.UserNotFound);

            await _userRepository.UnblockUser(userId);

            return new ErrorResult();
        }
    }
}
