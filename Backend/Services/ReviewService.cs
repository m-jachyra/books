using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Backend.Data.Entities;
using Backend.Helpers;
using Backend.Models;
using Backend.Models.Review;
using Backend.Repositories;
using Backend.Services.Base;
using Elastic.Clients.Elasticsearch;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class ReviewService : ServiceAsync<Review, ReviewListDto, ReviewDetailsDto, ReviewUpdateDto>
    {
        private readonly IRepositoryAsync<Review> _repository;
        private readonly IMapper _mapper;
        
        public ReviewService(IRepositoryAsync<Review> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ReviewListDto>> GetTopReviews()
        {
            var query = _repository.Entities.Include(x => x.UserReviewPluses);
            
            var result = await query
                .OrderByDescending(x => x.UserReviewPluses.Count())
                .ProjectTo<ReviewListDto>(_mapper.ConfigurationProvider)
                .Take(10)
                .ToListAsync();
            
            return result;
        }
        public async Task<PagedList<ReviewListDto>> GetByBookId(int id, PagedListQuery<Review> request)
        {
            var result = await GetAsync(review => review.BookId == id, request);

            return result;
        }
    }
}