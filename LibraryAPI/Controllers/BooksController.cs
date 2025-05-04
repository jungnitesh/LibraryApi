using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Swashbuckle.AspNetCore.Annotations;
using LibraryAPI.Domain.Models;
using LibraryAPI.Application.Dtos;
using LibraryAPI.Application.Contracts.ServiceContracts;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController(IBookService bookService) : ControllerBase
    {
        [HttpPost]
        [Route("AddBook")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(
         Summary = "Adds New book to Library",
         Description = "Requires both JWT Bearer token and API key with Book details. Stores Book in the database",
         OperationId = "AddBook",
         Tags = new[] { "Books" } 
)]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto addBookDto)
        {
             await bookService.AddBook(addBookDto);
             return Ok(new { message = "Book added successfully." });
        }

        [HttpPut]
        [Route("UpdateBook/{id}")]
        public async Task<ActionResult<UpdateBookDto>> UpdateBook(int id, [FromBody] UpdateBookDto updatedBookDto)
        {
           
                await bookService.UpdateBook(updatedBookDto);
                return Ok(updatedBookDto);
                // Log the exception (optional)
        }

        [HttpGet]
        [Route("GetBook/{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
           
                var book = await bookService.GetBook(id);
                return Ok(book);
           // Log the exception (optional)
        }

        [HttpDelete]
        [Route("DeleteBook/{id}")]
        public async Task<ActionResult<string>> DeleteBook(int id)
        {
                await bookService.DeleteBook(id);
                return Ok(new { message = "Book deleted successfully." });
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
                var books = await bookService.GetAllBooks();
                return Ok(books);
        }
    }
}