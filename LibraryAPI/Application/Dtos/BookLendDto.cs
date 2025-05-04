namespace LibraryAPI.Application.Dtos
{
    public class BookLendDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
    }
}
