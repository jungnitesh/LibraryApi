namespace LibraryAPI.Data.Dtos
{
    public class ReviewDto
    {
        public ReviewDto(int ratingValue, string commentText, string userId, DateTime UpdatedAt)
        {
            this.CommentText = commentText;
            this.RatingValue = ratingValue;
            this.UserId = userId;
            this.UpdatedAt = UpdatedAt;
        }
        public int RatingValue { get; set; }
        public string CommentText {  get; set; }
        public string UserId {  get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
