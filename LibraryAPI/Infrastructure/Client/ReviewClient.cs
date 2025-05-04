using LibraryAPI.Application.Contracts.ClientContracts;
using LibraryAPI.Domain.Models;
using LibraryAPI.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.Client
{
    public class ReviewClient(LibraryDbContext _context) : IReviewClient
    {
        public async Task<bool> AddReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> DeleteReview(int reviewId)
        {
            await _context.Reviews
                .Where(r => r.Id == reviewId)
                .ExecuteDeleteAsync();
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Review?> GetReview(int id)
        { 
            return await _context.Reviews
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Review>?> GetReviewsByBookId(int bookId)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }

        public async Task<bool> ReviewExists(int id)
        {
            return await _context.Reviews
                .AnyAsync(r => r.Id == id);
        }

        public async Task<bool> UpdateReview(Review review)
        {
            return await _context.TryUpdateAsync(review, review.Id);
        }
    }
}
