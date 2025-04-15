using LibraryAPI.Data;
using LibraryAPI.Domain.Models;
using LibraryAPI.Services.Contracts;
using Microsoft.EntityFrameworkCore;


namespace LibraryAPI.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryDbContext _context;

        public LibraryService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> LendBookAsync(BookLend bookLend)
        {
            var book = await _context.Books.FindAsync(bookLend.BookId);
            if (book == null || !book.IsAvailable)
            {
                return false; // Book not found or not available
            }

            book.IsAvailable = false;
            _context.BookLends.Add(bookLend);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReturnBookAsync(int bookLendId)
        {
            var bookLend = await _context.BookLends.Include(bl => bl.LentBook).FirstOrDefaultAsync(bl => bl.Id == bookLendId);
            if (bookLend == null || bookLend.ReturnedDate != null)
            {
                return false; // BookLend not found or already returned
            }

            bookLend.ReturnedDate = DateTime.UtcNow;
            bookLend.LentBook.IsAvailable = true;
            _context.BookLends.Update(bookLend);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> GetFeaturedBooks()
        {
            return await _context.Books.Where(b => b.IsFeatured).ToListAsync();
        }

        public async Task<bool> MarkBookAsFeatured()
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => !b.IsFeatured);
            if (book == null)
            {
                return false; // No book available to mark as featured
            }

            book.IsFeatured = true;
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}