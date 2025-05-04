using LibraryAPI.Application.Dtos;
using LibraryAPI.Domain.Models;

namespace LibraryAPI.Application.Contracts.ClientContracts
{
    public interface ILibraryClient
    {
        Task<bool> LendBookAsync(BookLend bookLend);
        Task<bool> ReturnBookAsync(int bookLendId,int bookId);
        Task<IEnumerable<Book>> GetFeaturedBooks();
        Task<bool> MarkBookAsFeatured(int bookId);
        
    }
}
