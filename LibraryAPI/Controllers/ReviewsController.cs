using LibraryAPI.Domain.Models;
using LibraryAPI.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController(IReviewService reviewService) : Controller
    {
        [HttpPost]
        [Route("CreateReview")]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            try
            {
                var result = await reviewService.AddReview(review);
                if (!result)
                {
                    return StatusCode(500, "Failed to create the review.");
                }
                return Ok(review);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while creating the review.");
            }
        }

        [HttpGet]
        [Route("GetReviewById/{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            try
            {
                var review = await reviewService.GetReview(id);
                if (review == null)
                {
                    return NotFound("Review not found.");
                }
                return Ok(review);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while retrieving the review.");
            }
        }

        [HttpPut]
        [Route("EditReview/{id}")]
        public async Task<IActionResult> EditReview(int id, [FromBody] Review updatedReview)
        {
            try
            {
                var existingReview = await reviewService.GetReview(id);
                if (existingReview == null)
                {
                    return NotFound("Review not found.");
                }

                existingReview.CommentText = updatedReview.CommentText;
                existingReview.RatingValue = updatedReview.RatingValue;
                existingReview.CreatedAt = updatedReview.CreatedAt;

                var result = await reviewService.UpdateReview(existingReview);
                if (!result)
                {
                    return StatusCode(500, "Failed to update the review.");
                }
                return Ok(existingReview);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while editing the review.");
            }
        }

        [HttpDelete]
        [Route("DeleteReview/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var review = await reviewService.GetReview(id);
                if (review == null)
                {
                    return NotFound("Review not found.");
                }

                var result = await reviewService.DeleteReview(review);
                if (!result)
                {
                    return StatusCode(500, "Failed to delete the review.");
                }
                return Ok("Review deleted successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while deleting the review.");
            }
        }
    }
}