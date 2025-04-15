using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI.Domain.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        [InverseProperty("ReviewdBook")]
        public virtual List<Review> Reviews { get; set; } = [];
        [InverseProperty("RequestedBook")]
        public virtual List<HoldRequest> HoldingRequests { get; set; } = new List<HoldRequest>();
        [NotMapped]
        public double AverageRating => Reviews.Count == 0 ? 0 : Reviews.Average(r => r.RatingValue);
    }




}
