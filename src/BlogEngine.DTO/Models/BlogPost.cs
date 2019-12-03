using System;

namespace BlogEngine.DTO.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public PostStatus Status { get; set; }
        public DateTime? PublicationDate { get; set; }
        public BlogUser Author { get; set; }
    }
}
