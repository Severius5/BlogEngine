namespace BlogEngine.Core.Results
{
    public class EditUserResult : ErrorResult
    {
        public int UserId { get; }

        public string UserSlug { get; }

        public EditUserResult(string errorCode)
        {
            ErrorCode = errorCode;
        }

        public EditUserResult(int userId, string userSlug)
        {
            UserId = userId;
            UserSlug = userSlug;
        }
    }
}
