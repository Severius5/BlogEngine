using Microsoft.AspNetCore.Authorization;

namespace BlogEngine.Authorization.Requirements
{
    public class PostOperationRequirement : IAuthorizationRequirement
    {
        public static readonly PostOperationRequirement View = new PostOperationRequirement(PostOperation.View);
        public static readonly PostOperationRequirement Edit = new PostOperationRequirement(PostOperation.Edit);
        public static readonly PostOperationRequirement Delete = new PostOperationRequirement(PostOperation.Delete);
        public static readonly PostOperationRequirement Remove = new PostOperationRequirement(PostOperation.Remove);
        public static readonly PostOperationRequirement Restore = new PostOperationRequirement(PostOperation.Restore);
        public static readonly PostOperationRequirement Publish = new PostOperationRequirement(PostOperation.Publish);
        public static readonly PostOperationRequirement Unpublish = new PostOperationRequirement(PostOperation.Unpublish);

        public PostOperation Operation { get; }

        public PostOperationRequirement(PostOperation operation)
        {
            Operation = operation;
        }
    }
}
