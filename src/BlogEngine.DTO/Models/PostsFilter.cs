namespace BlogEngine.DTO.Models
{
    public class PostsFilter
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; }
        public int? AuthorId { get; set; }
        public bool Drafted { get; set; } = true;
        public bool Published { get; set; } = true;
        public bool Removed { get; set; }
        public bool Own { get; set; }
    }
}
