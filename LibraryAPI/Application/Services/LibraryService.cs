using AutoMapper;
using LibraryAPI.Common.Exceptions;
using LibraryAPI.Application.Contracts.ClientContracts;
using LibraryAPI.Application.Contracts.ServiceContracts;
using LibraryAPI.Application.Dtos;
using LibraryAPI.Domain.Models;

namespace LibraryAPI.Application.Services
{
    public class LibraryService(ILibraryClient _libraryClient , IBookClient _bookClient, IMapper _mapper) : ILibraryService
    {
        public async Task<bool> LendBookAsync(BookLendDto bookLendDto)
        {
            if(!await _bookClient.BookExists(bookLendDto.BookId))
                throw new NotFoundException("Book not found");
            //perhaps check also user not found?
            var bookLend = _mapper.Map<BookLend>(bookLendDto);
           return await _libraryClient.LendBookAsync(bookLend);
        }

        public async Task<bool> ReturnBookAsync(BookReturnDto returnBookDto)
        {
            var bookLendExists = await _libraryClient.BookL(returnBookDto.BookLendId);
            return await _libraryClient.ReturnBookAsync(returnBookDto.BookLendId, returnBookDto.BookId);
        }

        public async Task<IEnumerable<BookDto>?> GetFeaturedBooks()
        {
            try
            {
                var books = await _libraryClient.GetFeaturedBooks();
                var bookDtos = _mapper.Map<List<BookDto>?>(books);
                return bookDtos;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error fetching featured books: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> MarkBookAsFeatured(int bookId)
        {
            var bookExists = await _bookClient.BookExists(bookId);
            if(!bookExists)
                throw new NotFoundException("Book not found");
            return await _libraryClient.MarkBookAsFeatured(bookId);
        }
    }
}