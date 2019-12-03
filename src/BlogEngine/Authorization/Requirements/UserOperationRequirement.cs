using Microsoft.AspNetCore.Authorization;

namespace BlogEngine.Authorization.Requirements
{
    public class UserOperationRequirement : IAuthorizationRequirement
    {
        public static readonly UserOperationRequirement Edit = new UserOperationRequirement(UserOperation.Edit);
        public static readonly UserOperationRequirement Block = new UserOperationRequirement(UserOperation.Block);
        public static readonly UserOperationRequirement Unblock = new UserOperationRequirement(UserOperation.Unblock);
        public static readonly UserOperationRequirement ChangeAdminStatus = new UserOperationRequirement(UserOperation.ChangeAdminStatus);

        public UserOperation Operation { get; }

        public UserOperationRequirement(UserOperation operation)
        {
            Operation = operation;
        }
    }
}
