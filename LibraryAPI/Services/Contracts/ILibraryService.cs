using LibraryAPI.Domain.Models;

namespace LibraryAPI.Services.Contracts
{
    public interface ILibraryService
    {
        Task<bool> LendBookAsync(BookLend bookLend);
        Task<bool> ReturnBookAsync(int bookLendId);
        Task<IEnumerable<Book>> GetFeaturedBooks();
        Task<bool> MarkBookAsFeatured();
    }
}
