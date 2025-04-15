
namespace LibraryAPI.Data.Dtos
{
    public class AddBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; }
        public string ImageUrl { get; set; }
    }
}
