using System;

namespace BlogEngine.DTO.Models
{
    public class BlogUser
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Slug { get; set; }
        public string Bio { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime CreationDate { get; set; }
        public string DetailsStamp { get; set; }
    }
}
