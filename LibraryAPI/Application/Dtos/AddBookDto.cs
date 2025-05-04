namespace LibraryAPI.Application.Dtos
{
    public class AddBookDto(string title, string author, string imageUrl)
    {
        public int Id { get; set; }
        public string Title { get; set; } = title;
        public string Author { get; set; } = author;
        public string ImageUrl { get; set; }= imageUrl;
    }
}
