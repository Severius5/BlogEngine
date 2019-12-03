using System.Collections.Generic;

namespace BlogEngine.Core.Results
{
    public class PagedResult<T> : ErrorResult
        where T : class
    {
        public IList<T> Elements { get; }
        public int TotalElements { get; }

        public PagedResult(string errorCode)
        {
            ErrorCode = errorCode;
        }

        public PagedResult(IList<T> elements, int totalElements)
        {
            Elements = elements;
            TotalElements = totalElements;
        }
    }
}
