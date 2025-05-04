using AutoMapper;
using LibraryAPI.Application.Contracts.ClientContracts;
using LibraryAPI.Application.Contracts.ServiceContracts;
using LibraryAPI.Application.Dtos;
using LibraryAPI.Common.Exceptions;
using LibraryAPI.Domain.Models;
using System.Linq.Expressions;

namespace LibraryAPI.Application.Services
{
    public class BookService(IBookClient client, IMapper _mapper) : IBookService
    {
        public async Task<bool> AddBook(AddBookDto addBookDto)
        {
            try
            {
                var book = _mapper.Map<Book>(addBookDto);
                return await client.AddBook(book);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<bool> UpdateBook(UpdateBookDto updatebookDto)
        {
            var bookExists = await client.BookExists(updatebookDto.Id);
            if(!bookExists)
                throw new NotFoundException("Book not found");
            var bookToUpdate = _mapper.Map<Book>(updatebookDto);
            return await client.UpdateBook(bookToUpdate);
        }

        public async Task<bool> DeleteBook(int bookId)
        {
            var bookExists = await client.BookExists(bookId);   
            if (!bookExists)
                throw new NotFoundException("Book not found");
            return await client.DeleteBook(bookId);
        }

        public async Task<BookDto?> GetBook(int id)
        {
            try
            {
                var book = await client.GetBook(id);
                var bookDto = _mapper.Map<BookDto>(book);
                return bookDto;
            }

            catch(Exception)
            {
                throw;
            }
        }

        public async Task<List<BookDto?>> GetAllBooks()
        {
            try
            {
                var books = await client.GetAllBooks();
                var bookDtos = _mapper.Map<List<BookDto?>>(books);
                return bookDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookDto?>> GetFeaturedBooks()
        {
            try
            {
                var books = await client.GetFeaturedBooks();
                var bookDtos = _mapper.Map<List<BookDto?>>(books);
                return bookDtos;
            }
            catch (Exception )
            {
                throw;
            }
        }

        public async Task<bool> BookExists(int id)
        {
            return await client.BookExists(id);
        }
    }
}