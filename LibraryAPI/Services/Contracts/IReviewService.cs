using LibraryAPI.Domain.Models;

namespace LibraryAPI.Services.Contracts
{
    public interface IReviewService
    {
        Task<bool> AddReview(Review review);
        Task<bool> UpdateReview(Review review);
        Task<bool> DeleteReview(Review review);
        Task<Review> GetReview(int id);
        Task<List<Review>> GetReviewsByBookId(int bookId);

    }
}
