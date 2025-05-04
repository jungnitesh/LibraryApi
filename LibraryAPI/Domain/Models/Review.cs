using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Domain.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; } 
        public int BookId { get; set; }
        public string UserId { get; set; }
        public string CommentText { get; set; } = string.Empty;
        [Range(1, 5)]
        public int RatingValue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("BookId")]
        public Book ReviewdBook { get; set; } = new Book();
    }
}
