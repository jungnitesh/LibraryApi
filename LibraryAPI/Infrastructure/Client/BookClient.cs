using LibraryAPI.Application.Contracts.ClientContracts;
using LibraryAPI.Common.Exceptions;
using LibraryAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.Client
{
    public class BookClient(LibraryDbContext _context) : IBookClient
    {

        public async Task<bool> AddBook(Book book)
        {
            await _context.Books.AddAsync(book);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateBook(Book book)
        {
 
           var bookInDb =  await _context.Books.FindAsync(book.Id) ?? throw new Exception("Book not found");
            _context.Entry(bookInDb).CurrentValues.SetValues(book);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id)?? throw new Exception("Book not found");
            _context.Books.Remove(book);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Book?> GetBook(int id)
        {
            try
            {
                return await _context.Books.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error fetching book from db", ex.InnerException ?? ex);
            }
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<List<Book>> GetFeaturedBooks()
        {
            return await _context.Books.Where(b => b.IsFeatured).ToListAsync();
        }


        public Task<bool> EntityExists(int entityId, Book Entity)
        {
            throw new NotImplementedException();
        }
    }
}
