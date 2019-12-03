using BlogEngine.DTO.Models;
using System;

namespace BlogEngine.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public PostStatus Status { get; set; }
        public DateTime? PublicationDate { get; set; }
        public UserViewModel Author { get; set; }

        public static implicit operator PostViewModel(BlogPost post)
        {
            if (post == null)
                return null;

            return new PostViewModel
            {
                Author = post.Author,
                Content = post.Content,
                Description = post.Description,
                Id = post.Id,
                PublicationDate = post.PublicationDate,
                Slug = post.Slug,
                Title = post.Title,
                Status = post.Status
            };
        }

        public static explicit operator BlogPost(PostViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return new BlogPost
            {
                Author = (BlogUser)model.Author,
                Content = model.Content,
                Description = model.Description,
                Id = model.Id,
                PublicationDate = model.PublicationDate,
                Slug = model.Slug,
                Status = model.Status,
                Title = model.Title
            };
        }
    }
}
