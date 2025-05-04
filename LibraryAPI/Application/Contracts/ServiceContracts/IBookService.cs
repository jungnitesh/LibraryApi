using LibraryAPI.Application.Dtos;
using LibraryAPI.Domain.Models;

namespace LibraryAPI.Application.Contracts.ServiceContracts
{
    public interface IBookService
    {
        Task<bool> AddBook(AddBookDto book);
        Task<bool> UpdateBook(UpdateBookDto book);
        Task<bool> DeleteBook(int id);
        Task<BookDto?> GetBook(int id);
        Task<List<BookDto?>> GetAllBooks();
        Task<List<BookDto?>> GetFeaturedBooks();

    }
}
