using LibraryAPI.Application.Contracts.ServiceContracts;
using LibraryAPI.Application.Dtos;
using LibraryAPI.Domain.Models;
using Microsoft.AspNetCore.Mvc;
namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController(ILibraryService libraryService) : Controller
    {
        [HttpPost]
        [Route("LendBook")]
        public async Task<IActionResult> LendBook([FromBody] BookLendDto bookLendDto)
        {
            
                var result = await libraryService.LendBookAsync(bookLendDto);
                if (!result)
                {
                    return BadRequest("Failed to lend the book. The book might not be available.");
                }
                return Ok("Book lent successfully.");
        }

        [HttpPost]
        [Route("ReturnBook/{bookLendId}")]
        public async Task<IActionResult> ReturnBook([FromBody] BookReturnDto bookReturnDto)
        {
                var result = await libraryService.ReturnBookAsync(bookReturnDto);
                if (!result)
                {
                    return BadRequest("Failed to return the book. The book lend record might not exist or the book is already returned.");
                }
                return Ok("Book returned successfully.");
        }

        [HttpGet]
        [Route("GetFeaturedBooks")]
        public async Task<IActionResult> GetFeaturedBooks()
        {
                var featuredBooks = await libraryService.GetFeaturedBooks();
                return Ok(featuredBooks);
        }

        [HttpPost]
        [Route("MarkBookAsFeatured")]
        public async Task<IActionResult> MarkBookAsFeatured(int bookId)
        {
                var result = await libraryService.MarkBookAsFeatured(bookId);
                if (!result)
                {
                    return BadRequest("Failed to mark a book as featured. No eligible book found.");
                }
                return Ok("Book marked as featured successfully.");
           
        }
    }
}