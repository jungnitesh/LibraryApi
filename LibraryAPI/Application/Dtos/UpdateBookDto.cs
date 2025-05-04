namespace LibraryAPI.Application.Dtos
{
    public class UpdateBookDto(string title, string description, string author, DateTime updated, DateTime lastUpdated)
    {
        public int Id { get; set; }
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public string Author { get; set; } = author;
        public DateTime Updated { get; set; } = updated;
        public DateTime LastUpdated { get; set; } = lastUpdated;
    }
}
