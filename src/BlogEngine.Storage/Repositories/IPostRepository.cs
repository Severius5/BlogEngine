using BlogEngine.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Storage.Repositories
{
    public interface IPostRepository
    {
        Task<int> CreatePost(BlogPost post);
        Task<(IList<BlogPost> posts, int totalPosts)> GetPosts(PostsFilter filter);
        Task<BlogPost> GetPost(int id);
        Task EditPost(BlogPost post);
        Task DeletePost(int id);
        Task ChangePostStatus(int id, PostStatus newStatus);
    }
}
