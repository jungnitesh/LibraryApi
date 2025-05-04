using LibraryAPI.Application.Dtos;
using LibraryAPI.Domain.Models;

namespace LibraryAPI.Application.Contracts.ServiceContracts
{
    public interface IReviewService
    {
        Task<bool> AddReview(ReviewDto review);
        Task<bool> UpdateReview(ReviewDto review);
        Task<bool> DeleteReview(int ReviewId);
        Task<ReviewDto?> GetReview(int id);
        Task<List<ReviewDto>?> GetReviewsByBookId(int bookId);

    }
}
