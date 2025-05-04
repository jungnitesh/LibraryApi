namespace LibraryAPI.Application.Dtos
{
    public class ReviewDto(int ratingValue, string commentText, string userId, DateTime UpdatedAt)
    {
        public int Id { get; set; }
        public int RatingValue { get; set; } = ratingValue;
        public string CommentText { get; set; } = commentText;
        public string UserId { get; set; } = userId;
        public DateTime UpdatedAt { get; set; } = UpdatedAt;
    }
}
