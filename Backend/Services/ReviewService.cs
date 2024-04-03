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
            var query = GetQueryable();

            query = query
                .OrderByDescending(x => x.UserReviewPluses.Count())
                .Take(10);
            
            return await GetAsync(query);
        }
        public async Task<PagedList<ReviewListDto>> GetByBookId(int id, PagedListQuery<Review> request)
        {
            var query = GetQueryable();

            query = query.Where(x => x.BookId == id);
            
            return await GetAsync(query, request);
        }
        
        public new async Task<PagedList<ReviewListDto>> GetAsync(PagedListQuery<Review> request)
        {
            return await GetAsync(GetQueryable(), request);
        }
        
        public new async Task<ReviewDetailsDto> GetByIdAsync(int id)
        {
            return await GetByIdAsync(GetQueryable(), id);
        }
        
        private IQueryable<Review> GetQueryable()
        {
            var query = _repository.Entities;
            
            return query
                .Include(x => x.UserReviewPluses)
                .Include(x => x.User);
        }
    }
}