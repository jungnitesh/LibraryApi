namespace LibraryAPI.Application.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        public double AverageRating { get; set; } = 0;
    }
}
