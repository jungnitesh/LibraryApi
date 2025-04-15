using AutoMapper;
using LibraryAPI.Data;
using LibraryAPI.Data.Dtos;
using LibraryAPI.Domain.Models;
using LibraryAPI.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class BookService(LibraryDbContext context, IMapper _mapper) : IBookService
    {
        public async Task<int> AddBook(AddBookDto addBookDto)
        {
            var bookToAdd = _mapper.Map<Book>(addBookDto);
            await context.Books.AddAsync(bookToAdd);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateBook(UpdateBookDto updatebookDto)
        {
            // Check if the book exists
            var existingBook = await context.Books.FindAsync(updatebookDto.BookId) ?? throw new Exception("Book not found");
            _mapper.Map(updatebookDto, existingBook);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteBook(int bookId)
        {
            try
            {
                var existingBook = await context.Books.FindAsync(bookId) ?? throw new Exception("Book Not Found");
                context.Books.Remove(existingBook);
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                throw new Exception("An error occurred while deleting the book.", ex);
            }
            
        }

        public async Task<Book?> GetBook(int id) // Updated return type to Book?
        {
            var book = await context.Books.FindAsync(id);
            return book; 
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await context.Books.ToListAsync();
        }

        public async Task<List<Book>> GetFeaturedBooks()
        {
            return await context.Books
                .Where(b => b.IsFeatured)
                .ToListAsync();
        }
    }
}