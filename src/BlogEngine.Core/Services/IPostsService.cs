using BlogEngine.Core.Results;
using BlogEngine.DTO.Models;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services
{
    public interface IPostsService
    {
        Task<PagedResult<BlogPost>> GetPosts(PostsFilter filter);
        Task<CreatePostResult> CreatePost(string title, string content, string description, bool publish, int authorId);
        Task<BlogPost> GetPost(int id);
        Task<EditPostResult> EditPost(int id, string title, string content, string description);
        Task<ErrorResult> DeletePost(int id);
        Task<ErrorResult> RemovePost(int id);
        Task<ErrorResult> PublishPost(int id);
        Task<ErrorResult> UnpublishPost(int id);
        Task<ErrorResult> RestorePost(int id);
    }
}
