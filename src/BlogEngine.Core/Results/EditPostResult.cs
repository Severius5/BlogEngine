namespace BlogEngine.Core.Results
{
    public class EditPostResult : ErrorResult
    {
        public int PostId { get; }

        public string PostSlug { get; }

        public EditPostResult(string errorCode)
        {
            ErrorCode = errorCode;
        }

        public EditPostResult(int id, string postSlug)
        {
            PostId = id;
            PostSlug = postSlug;
        }
    }
}
