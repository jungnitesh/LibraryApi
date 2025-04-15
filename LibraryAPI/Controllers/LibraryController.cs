using LibraryAPI.Domain.Models;
using LibraryAPI.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController(ILibraryService libraryService) : Controller
    {
        [HttpPost]
        [Route("LendBook")]
        public async Task<IActionResult> LendBook([FromBody] BookLend bookLend)
        {
            try
            {
                var result = await libraryService.LendBookAsync(bookLend);
                if (!result)
                {
                    return BadRequest("Failed to lend the book. The book might not be available.");
                }
                return Ok("Book lent successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while lending the book.");
            }
        }

        [HttpPost]
        [Route("ReturnBook/{bookLendId}")]
        public async Task<IActionResult> ReturnBook(int bookLendId)
        {
            try
            {
                var result = await libraryService.ReturnBookAsync(bookLendId);
                if (!result)
                {
                    return BadRequest("Failed to return the book. The book lend record might not exist or the book is already returned.");
                }
                return Ok("Book returned successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while returning the book.");
            }
        }

        [HttpGet]
        [Route("GetFeaturedBooks")]
        public async Task<IActionResult> GetFeaturedBooks()
        {
            try
            {
                var featuredBooks = await libraryService.GetFeaturedBooks();
                return Ok(featuredBooks);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while retrieving featured books.");
            }
        }

        [HttpPost]
        [Route("MarkBookAsFeatured")]
        public async Task<IActionResult> MarkBookAsFeatured()
        {
            try
            {
                var result = await libraryService.MarkBookAsFeatured();
                if (!result)
                {
                    return BadRequest("Failed to mark a book as featured. No eligible book found.");
                }
                return Ok("Book marked as featured successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while marking a book as featured.");
            }
        }
    }
}