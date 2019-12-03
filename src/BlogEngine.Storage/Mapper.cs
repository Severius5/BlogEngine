using BlogEngine.DTO.Models;
using BlogEngine.Storage.Entities;

namespace BlogEngine.Storage
{
    internal static class Mapper
    {
        public static Post ConvertToEntity(BlogPost model)
        {
            if (model == null)
                return null;

            return new Post
            {
                Content = model.Content,
                Description = model.Description,
                Id = model.Id,
                PublicationDate = model.PublicationDate,
                Slug = model.Slug,
                Status = model.Status,
                Title = model.Title,
                AuthorId = model.Author.Id,
                NormalizedTitle = model.Title.ToUpperInvariant()
            };
        }

        public static User ConvertToEntity(BlogUser model)
        {
            if (model == null)
                return null;

            return new User
            {
                Bio = model.Bio,
                CreationDate = model.CreationDate,
                Email = model.Email,
                Id = model.Id,
                IsAdmin = model.IsAdmin,
                IsBlocked = model.IsBlocked,
                NormalizedEmail = model.Email.ToUpperInvariant(),
                NormalizedUsername = model.Username.ToUpperInvariant(),
                Password = model.Password,
                Slug = model.Slug,
                Username = model.Username,
                DetailsStamp = model.DetailsStamp
            };
        }

        public static BlogUser ConvertToModel(User entity)
        {
            if (entity == null)
                return null;

            return new BlogUser
            {
                Email = entity.Email,
                Id = entity.Id,
                IsAdmin = entity.IsAdmin,
                IsBlocked = entity.IsBlocked,
                Password = entity.Password,
                Username = entity.Username,
                Bio = entity.Bio,
                CreationDate = entity.CreationDate,
                Slug = entity.Slug,
                DetailsStamp = entity.DetailsStamp
            };
        }

        public static BlogPost ConvertToModel(Post entity)
        {
            if (entity == null)
                return null;

            var author = ConvertToModel(entity.Author);
            author = author ?? new BlogUser { Id = entity.AuthorId };

            return new BlogPost
            {
                Author = author,
                Content = entity.Content,
                Description = entity.Description,
                Id = entity.Id,
                PublicationDate = entity.PublicationDate,
                Slug = entity.Slug,
                Title = entity.Title,
                Status = entity.Status
            };
        }
    }
}
