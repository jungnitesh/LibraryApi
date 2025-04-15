using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Domain.Models
{
    public class HoldRequest
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("BookId")]
        public int BookId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public Book RequestedBook { get; set; }

    }
}
