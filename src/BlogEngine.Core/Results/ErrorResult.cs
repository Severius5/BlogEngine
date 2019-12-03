using System;

namespace BlogEngine.Core.Results
{
    public class ErrorResult
    {
        public string ErrorCode { get; protected set; }

        public bool IsError => !string.IsNullOrEmpty(ErrorCode);

        public ErrorResult(string errorCode)
        {
            ErrorCode = errorCode;
        }

        public ErrorResult()
        {
        }
    }
}
