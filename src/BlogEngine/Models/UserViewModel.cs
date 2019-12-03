using BlogEngine.DTO.Models;
using System;

namespace BlogEngine.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Slug { get; set; }
        public string Bio { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime CreationDate { get; set; }

        public static implicit operator UserViewModel(BlogUser user)
        {
            if (user == null)
                return null;

            return new UserViewModel
            {
                Bio = user.Bio,
                CreationDate = user.CreationDate,
                Email = user.Email,
                Id = user.Id,
                IsAdmin = user.IsAdmin,
                IsBlocked = user.IsBlocked,
                Slug = user.Slug,
                Username = user.Username
            };
        }

        public static explicit operator BlogUser(UserViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return new BlogUser
            {
                Bio = model.Bio,
                CreationDate = model.CreationDate,
                Email = model.Email,
                Id = model.Id,
                IsAdmin = model.IsAdmin,
                IsBlocked = model.IsBlocked,
                Slug = model.Slug,
                Username = model.Username
            };
        }
    }
}
