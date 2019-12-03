using BlogEngine.DTO.Models;
using X.PagedList;

namespace BlogEngine.Models
{
    public class UsersListViewModel
    {
        public IPagedList<UserViewModel> Users { get; set; }
        public UsersFilter Filter { get; set; }
    }
}
