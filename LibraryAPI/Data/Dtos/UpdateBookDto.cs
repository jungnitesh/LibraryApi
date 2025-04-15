namespace LibraryAPI.Data.Dtos
{
    public class UpdateBookDto
    {
        public string Title { get; set; }   
        public string Description { get; set; } 
        public string Author { get; set; }  
        public DateTime Updated { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; }
        public int BookId { get; set; }
    }
}
