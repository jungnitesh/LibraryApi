using LibraryAPI.Domain.Models;

namespace LibraryAPI.Application.Contracts.ClientContracts
{
    public interface IReviewClient
    {
        Task<bool> AddReview(Review review);
        Task<bool> UpdateReview(Review review);
        Task<bool> DeleteReview(int reviewId);
        Task<Review?> GetReview(int id);
        Task<bool> ReviewExists(int id);
        Task<List<Review>?> GetReviewsByBookId(int bookId);
    }
}
