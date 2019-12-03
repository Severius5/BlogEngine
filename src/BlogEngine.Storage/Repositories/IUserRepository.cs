using BlogEngine.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Storage.Repositories
{
    public interface IUserRepository
    {
        Task<BlogUser> GetUser(string email);
        Task<BlogUser> GetUser(int id);
        Task<(IList<BlogUser> users, int totalUsers)> GetUsers(UsersFilter filter);
        Task ChangeUserAdminStatus(int userId, bool isAdmin);
        Task EditUser(BlogUser existingUser);
        Task BlockUser(int userId);
        Task UnblockUser(int userId);
    }
}
