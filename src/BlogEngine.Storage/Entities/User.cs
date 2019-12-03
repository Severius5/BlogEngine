using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngine.Storage.Entities
{
    [Table("users")]
    internal class User
    {
        public User()
        {
            CreatedPosts = new HashSet<Post>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string NormalizedEmail { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string NormalizedUsername { get; set; }

        [Required]
        [StringLength(100)]
        public string Slug { get; set; }

        [StringLength(1024)]
        public string Bio { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime CreationDate { get; set; }

        public string DetailsStamp { get; set; }

        [InverseProperty(nameof(Post.Author))]
        public ICollection<Post> CreatedPosts { get; set; }
    }
}
