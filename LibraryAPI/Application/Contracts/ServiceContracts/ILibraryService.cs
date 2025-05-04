using LibraryAPI.Application.Dtos;
using LibraryAPI.Domain.Models;

namespace LibraryAPI.Application.Contracts.ServiceContracts
{
    public interface ILibraryService
    {
        Task<bool> LendBookAsync(BookLendDto bookLendDto);
        Task<bool> ReturnBookAsync(BookReturnDto returnBookDto);
        Task<IEnumerable<BookDto>?> GetFeaturedBooks();
        Task<bool> MarkBookAsFeatured(int bookId);
    }
}
