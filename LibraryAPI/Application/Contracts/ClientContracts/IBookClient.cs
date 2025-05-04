using LibraryAPI.Application.Dtos;
using LibraryAPI.Domain.Models;

namespace LibraryAPI.Application.Contracts.ClientContracts
{
    public interface IBookClient : IBaseClient<Book>
    {
        Task<bool> AddBook(Book book);
        Task<bool> UpdateBook(Book book);
        Task<bool> DeleteBook(int id);
        Task<Book?> GetBook(int id);
        Task<bool> BookExists(int id);
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetFeaturedBooks();
    }
}
