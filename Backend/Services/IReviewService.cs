using Backend.Data.Entities;
using Backend.Models;
using Backend.Services.Base;

namespace Backend.Services
{
    public interface IReviewService : IServiceAsync<Review, ReviewDto>
    {
        Task<ReviewDto> GetByBookId(int id, int pageIndex, int pageSize);
    }
}