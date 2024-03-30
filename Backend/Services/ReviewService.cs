using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Backend.Data.Entities;
using Backend.Helpers;
using Backend.Models;
using Backend.Repositories;
using Backend.Services.Base;
using Elastic.Clients.Elasticsearch;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class ReviewService : ServiceAsync<Review, ReviewDto>, IReviewService
    {
        private readonly IRepositoryAsync<Review> _repository;
        private readonly IMapper _mapper;
        
        public ReviewService(IRepositoryAsync<Review> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ReviewDto>> GetTopReviews()
        {
            var query = _repository.Entities.Include(x => x.UserReviewPluses);
            
            var result = await query
                .OrderByDescending(x => x.UserReviewPluses.Count())
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .Take(10)
                .ToListAsync();
            
            return result;
        }
        public async Task<PagedList<ReviewDto>> GetByBookId(int id, PagedListQuery<Review> request)
        {
            var result = await GetAsync(review => review.BookId == id, request);

            return result;
        }
    }
}