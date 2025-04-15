using LibraryAPI.Data;
using LibraryAPI.Domain.Models;
using LibraryAPI.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly LibraryDbContext _context;

        public ReviewService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateReview(Review review)
        {
            var existingReview = await _context.Reviews.FindAsync(review.Id);
            if (existingReview == null)
            {
                return false;
            }

            existingReview.CommentText = review.CommentText;
            existingReview.RatingValue = review.RatingValue;
            existingReview.CreatedAt = review.CreatedAt;

            _context.Reviews.Update(existingReview);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteReview(Review review)
        {
            var existingReview = await _context.Reviews.FindAsync(review.Id);
            if (existingReview == null)
            {
                return false;
            }

            _context.Reviews.Remove(existingReview);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Review> GetReview(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<List<Review>> GetReviewsByBookId(int bookId)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }
    }
}