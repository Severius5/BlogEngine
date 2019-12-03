using BlogEngine.Core.Results;
using BlogEngine.DTO.Models;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services
{
    public interface IUsersService
    {
        Task<PagedResult<BlogUser>> GetUsers(UsersFilter filter);
        Task<BlogUser> GetUser(int userId);
        Task<ErrorResult> ChangeUserAdminStatus(int userId, bool isAdmin);
        Task<EditUserResult> EditUser(int userId, string username, string bio);
        Task<ErrorResult> BlockUser(int userId);
        Task<ErrorResult> UnblockUser(int userId);
    }
}
