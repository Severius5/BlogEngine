using BlogEngine.Core.Results;
using BlogEngine.DTO;
using BlogEngine.DTO.Models;
using BlogEngine.Storage.Repositories;
using System;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services.Internals
{
    internal class PostsService : IPostsService
    {
        private readonly IPostRepository _postRepository;

        public PostsService(IPostRepository postRepository)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        }

        public async Task<ErrorResult> PublishPost(int id)
        {
            var existingPost = await _postRepository.GetPost(id);
            if (existingPost == null)
                return new ErrorResult(ErrorCodes.PostNotFound);

            await _postRepository.ChangePostStatus(id, PostStatus.Published);

            return new ErrorResult();
        }

        public async Task<ErrorResult> UnpublishPost(int id)
        {
            var existingPost = await _postRepository.GetPost(id);
            if (existingPost == null)
                return new ErrorResult(ErrorCodes.PostNotFound);

            await _postRepository.ChangePostStatus(id, PostStatus.Draft);

            return new ErrorResult();
        }

        public async Task<ErrorResult> RestorePost(int id)
        {
            var existingPost = await _postRepository.GetPost(id);
            if (existingPost == null)
                return new ErrorResult(ErrorCodes.PostNotFound);

            await _postRepository.ChangePostStatus(id, PostStatus.Draft);

            return new ErrorResult();
        }

        public async Task<ErrorResult> RemovePost(int id)
        {
            var existingPost = await _postRepository.GetPost(id);
            if (existingPost == null)
                return new ErrorResult(ErrorCodes.PostNotFound);

            await _postRepository.ChangePostStatus(id, PostStatus.Removed);

            return new ErrorResult();
        }

        public async Task<ErrorResult> DeletePost(int id)
        {
            var existingPost = await _postRepository.GetPost(id);
            if (existingPost == null)
                return new ErrorResult(ErrorCodes.PostNotFound);

            await _postRepository.DeletePost(id);

            return new ErrorResult();
        }

        public async Task<CreatePostResult> CreatePost(string title, string content, string description, bool publish, int authorId)
        {
            var post = new BlogPost
            {
                Author = new BlogUser { Id = authorId },
                Content = content,
                Description = description,
                Status = publish ? PostStatus.Published : PostStatus.Draft,
                PublicationDate = publish ? DateTime.UtcNow : (DateTime?)null,
                Title = title,
                Slug = title.ToSlug()
            };

            var postId = await _postRepository.CreatePost(post);

            return new CreatePostResult(postId, post.Slug);
        }

        public async Task<EditPostResult> EditPost(int id, string title, string content, string description)
        {
            var existingPost = await _postRepository.GetPost(id);
            if (existingPost == null)
                return new EditPostResult(ErrorCodes.PostNotFound);

            var slug = title.ToSlug();

            existingPost.Slug = slug;
            existingPost.Title = title;
            existingPost.Content = content;
            existingPost.Description = description;

            await _postRepository.EditPost(existingPost);

            return new EditPostResult(id, slug);
        }

        public Task<BlogPost> GetPost(int id)
        {
            return _postRepository.GetPost(id);
        }

        public async Task<PagedResult<BlogPost>> GetPosts(PostsFilter filter)
        {
            var (posts, totalPosts) = await _postRepository.GetPosts(filter);

            return new PagedResult<BlogPost>(posts, totalPosts);
        }
    }
}
