using LibraryAPI.Application.Contracts.ClientContracts;
using LibraryAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.Client
{
    public class LibraryClient(LibraryDbContext _context) : ILibraryClient
    {
        public async Task<bool> LendBookAsync(BookLend bookLend)
        {
            await _context.BookLends.AddAsync(bookLend);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ReturnBookAsync(int bookLendId, int bookId)
        {
            var bookLend = await _context.BookLends.FindAsync(bookLendId);
            if (bookLend == null) return false;
            bookLend.ReturnedDate = DateTime.UtcNow;
            var book = await _context.Books.FindAsync(bookId) ?? throw new Exception("Book not found");
            book.IsAvailable = true;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Book>> GetFeaturedBooks()
        {
            return await _context.Books.Where(b => b.IsFeatured).ToListAsync();
        }

        public async Task<bool> MarkBookAsFeatured(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId)??throw new Exception("Book not found");
            book.IsFeatured = true;
            return await _context.SaveChangesAsync() > 0;
        }
       
    }
    
}
