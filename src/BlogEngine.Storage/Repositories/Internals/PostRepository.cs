using BlogEngine.DTO.Models;
using BlogEngine.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace BlogEngine.Storage.Repositories.Internals
{
    internal class PostRepository : IPostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task ChangePostStatus(int id, PostStatus newStatus)
        {
            var entity = new Post
            {
                Id = id,
                Status = newStatus
            };

            _context.Attach(entity);
            _context.Entry(entity).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
        }

        public async Task<int> CreatePost(BlogPost post)
        {
            var entity = Mapper.ConvertToEntity(post);
            entity.Id = 0;

            _context.Posts.Add(entity);

            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task EditPost(BlogPost post)
        {
            var entity = Mapper.ConvertToEntity(post);

            _context.Attach(entity);
            _context.Entry(entity).Property(x => x.Content).IsModified = true;
            _context.Entry(entity).Property(x => x.Description).IsModified = true;
            _context.Entry(entity).Property(x => x.NormalizedTitle).IsModified = true;
            _context.Entry(entity).Property(x => x.Slug).IsModified = true;
            _context.Entry(entity).Property(x => x.Title).IsModified = true;

            await _context.SaveChangesAsync();
        }

        public async Task<BlogPost> GetPost(int id)
        {
            var entity = await _context.Posts
                .AsNoTracking()
                .Include(x => x.Author)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return Mapper.ConvertToModel(entity);
        }

        public async Task<(IList<BlogPost> posts, int totalPosts)> GetPosts(PostsFilter filter)
        {
            // there are no posts w/o status
            if (!filter.Drafted && !filter.Published && !filter.Removed)
                return (null, 0);

            var query = _context.Posts
              .AsNoTracking();

            query = QueryPostsByStatus(query, filter.Drafted, filter.Published, filter.Removed);
            query = QueryPostsByAuthorId(query, filter.AuthorId);
            query = QueryPostsBySearchText(query, filter.Search);

            List<Post> posts = null;

            var count = await query.CountAsync();
            if (count != 0)
            {
                var skip = (filter.Page - 1) * filter.PageSize;
                posts = await query
                    .Include(x => x.Author)
                    .OrderBy(x => x.Id)
                    .Skip(skip)
                    .Take(filter.PageSize)
                    .ToListAsync();
            }

            var dtoPosts = posts?.Select(x => Mapper.ConvertToModel(x))?.ToList();

            return (dtoPosts, count);
        }

        public async Task DeletePost(int id)
        {
            await _context.Posts.Where(x => x.Id == id).DeleteAsync();
        }

        private IQueryable<Post> QueryPostsByStatus(IQueryable<Post> query, bool drafted, bool published, bool removed)
        {
            if (drafted && published && removed)
                return query;

            var predicate = PredicateBuilder.False<Post>();

            if (drafted)
                predicate = predicate.Or(x => x.Status == PostStatus.Draft);
            if (published)
                predicate = predicate.Or(x => x.Status == PostStatus.Published);
            if (removed)
                predicate = predicate.Or(x => x.Status == PostStatus.Removed);

            return query.Where(predicate);
        }

        private IQueryable<Post> QueryPostsByAuthorId(IQueryable<Post> query, int? authorId)
        {
            if (authorId.HasValue)
                return query.Where(x => x.AuthorId == authorId.Value);
            else
                return query;
        }

        private IQueryable<Post> QueryPostsBySearchText(IQueryable<Post> query, string searchText)
        {
            searchText = searchText?.ToUpperInvariant();
            if (string.IsNullOrWhiteSpace(searchText))
                return query;
            else
                return query.Where(x => x.NormalizedTitle.Contains(searchText));
        }
    }
}
