using LibraryAPI.Data.Dtos;
using LibraryAPI.Domain.Models;

namespace LibraryAPI.Services.Contracts
{
    public interface IBookService
    {
        Task<int> AddBook(AddBookDto book);
        Task<int> UpdateBook(UpdateBookDto book);
        Task<int> DeleteBook(int id);
        Task<Book?> GetBook(int id);
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetFeaturedBooks();

    }
}
