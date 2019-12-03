using BlogEngine.DTO.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngine.Storage.Entities
{
    [Table("posts")]
    internal class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [StringLength(150)]
        [Required]
        public string Title { get; set; }

        [StringLength(150)]
        [Required]
        public string NormalizedTitle { get; set; }

        [StringLength(150)]
        [Required]
        public string Slug { get; set; }

        [Required]
        [StringLength(450)]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public PostStatus Status { get; set; }

        public DateTime? PublicationDate { get; set; }

        [ForeignKey(nameof(AuthorId))]
        [InverseProperty(nameof(User.CreatedPosts))]
        public User Author { get; set; }
    }
}
