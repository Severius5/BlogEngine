using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Models
{
    public class CreatePostViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public bool Publish { get; set; }
    }
}
