using Backend.Data.Entities;
using Backend.Helpers;
using Backend.Models;
using Backend.Services.Base;

namespace Backend.Services
{
    public interface IReviewService : IServiceAsync<Review, ReviewDto>
    {
        Task<List<ReviewDto>> GetTopReviews();
        Task<PagedList<ReviewDto>> GetByBookId(int id, PagedListQuery<Review> request);
    }
}