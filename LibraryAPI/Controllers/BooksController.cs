using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using LibraryAPI.Data.Dtos;
using AutoMapper;
using Swashbuckle.Swagger.Annotations;

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
    Summary = "Test endpoint with both API key and JWT",
    Description = "Requires both JWT Bearer token and API key"
)]
        [SwaggerSecurityRequirement("Bearer")]
        [SwaggerSecurityRequirement("ApiKey")]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto addBookDto)
        {
            try
            {
                
                var result = await bookService.AddBook(addBookDto);
                if (result == 0)
                {
                    return StatusCode(500, "Failed to add the book.");
                }
                return Ok(addBookDto);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while adding the book.");
            }
        }

        [HttpPut]
        [Route("UpdateBook/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updatedBookDto)
        {
            try
            {
                
                var result = await bookService.UpdateBook(updatedBookDto);
                if (result == 0)
                {
                    return StatusCode(500, "Failed to update the book.");
                }
                return Ok(updatedBookDto);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while updating the book.");
            }
        }

        [HttpGet]
        [Route("GetBook/{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            try
            {
                var book = await bookService.GetBook(id);
                if (book == null)
                {
                    return NotFound("Book not found.");
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while retrieving the book.");
            }
        }

        [HttpDelete]
        [Route("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await bookService.DeleteBook(id);
                return Ok("Book deleted successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while deleting the book.");
            }
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await bookService.GetAllBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while retrieving the books.");
            }
        }
    }
}