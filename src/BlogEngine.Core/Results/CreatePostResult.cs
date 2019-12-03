namespace BlogEngine.Core.Results
{
    public class CreatePostResult : ErrorResult
    {
        public int PostId { get; }
        public string PostSlug { get; }

        public CreatePostResult(string errorCode)
        {
            ErrorCode = errorCode;
        }

        public CreatePostResult(int postId, string postSlug)
        {
            PostId = postId;
            PostSlug = postSlug;
        }
    }
}
