using AutoMapper;
using LibraryAPI.Common.Exceptions;
using LibraryAPI.Application.Contracts.ClientContracts;
using LibraryAPI.Application.Contracts.ServiceContracts;
using LibraryAPI.Application.Dtos;
using LibraryAPI.Common;
using LibraryAPI.Domain.Models;

namespace LibraryAPI.Application.Services
{
    public class ReviewService(IReviewClient _client, IMapper mapper) : IReviewService
    {
        public async Task<bool> AddReview(ReviewDto reviewDto)
        {
            var review = mapper.Map<Review>(reviewDto);
            return await _client.AddReview(review);
        }

        public async Task<bool> UpdateReview(ReviewDto reviewDto)
        {
            var reviewExists = await _client.ReviewExists(reviewDto.Id);
            if (!reviewExists)
            {
                throw new NotFoundException(Constants.ReviewNotFoundText);
            }
            var review = mapper.Map<Review>(reviewDto);
            return await _client.UpdateReview(review);
        }

        public async Task<bool> DeleteReview(int reviewId)
        {
            var reviewExists = await _client.ReviewExists(reviewId);
            if (!reviewExists)
            {
                throw new NotFoundException(Constants.ReviewNotFoundText);
            }
          
            return await _client.DeleteReview(reviewId);
        }

        public async Task<ReviewDto?> GetReview(int id)
        {
            var review = await _client.GetReview(id) ?? throw new Exception(Constants.ReviewNotFoundText);
            var reviewDto = mapper.Map<ReviewDto>(review);
            return reviewDto;
        }

        public async Task<List<ReviewDto>?> GetReviewsByBookId(int bookId)
        {
            var reviews = await _client.GetReviewsByBookId(bookId);
            var reviewsDtos = mapper.Map<List<ReviewDto>?>(reviews);
            return reviewsDtos;
        }
    }

}