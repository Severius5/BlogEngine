using BlogEngine.DTO.Models;
using X.PagedList;

namespace BlogEngine.Models
{
    public class PostsListViewModel
    {
        public IPagedList<PostViewModel> Posts { get; set; }
        public PostsFilter Filter { get; set; }
    }
}
