using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Backend.Data;
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
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        
        public ReviewService(IRepositoryAsync<Review> repository, IMapper mapper, AppDbContext context) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }
        
        public async Task<List<ReviewListDto>> GetTopReviews(int userId)
        {
           
            var query = GetQueryable();

            query = query
                .OrderByDescending(x => x.UserReviewPluses.Count())
                .Take(10);
            
            var configuration = new MapperConfiguration(cfg => cfg.CreateProjection<Review, ReviewListDto>()
                .ForMember(d => d.IsPlussed, o => o.MapFrom(s => s.UserReviewPluses.Any(x => x.UserId == userId))));
            
            var result = query.ProjectTo<ReviewListDto>(configuration);
            return await result.ToListAsync();
        }

        public async Task<List<ReviewListDto>> GetTopReviews()
        {
            var query = GetQueryable();

            query = query
                .OrderByDescending(x => x.UserReviewPluses.Count())
                .Take(10);
            
            return await GetAsync(query);
        }
        
        public async Task<PagedList<ReviewListDto>> GetByBookId(int id, PagedListQuery<Review> request, int userId)
        {
            var query = GetQueryable();

            query = query.Where(x => x.BookId == id);
            
            if (request.SortOrder?.ToLower() == "desc")
                query = query.OrderByDescending(Review.GetSortProperty(request.SortColumn));
            else
                query = query.OrderBy(Review.GetSortProperty(request.SortColumn));
            
            var configuration = new MapperConfiguration(cfg => cfg.CreateProjection<Review, ReviewListDto>()
                .ForMember(d => d.IsPlussed, o => o.MapFrom(s => s.UserReviewPluses.Any(x => x.UserId == userId))));
            
            var result = query.ProjectTo<ReviewListDto>(configuration);
            return await PagedList<ReviewListDto>.CreateAsync(result, request.Page, request.PageSize);
        }
        
        public async Task<PagedList<ReviewListDto>> GetByBookId(int id, PagedListQuery<Review> request)
        {
            var query = GetQueryable();

            query = query.Where(x => x.BookId == id);
            
            return await GetAsync(query, request);
        }
        
        public new async Task<PagedList<ReviewListDto>> GetAsync(PagedListQuery<Review> request, int userId)
        {
            var query = GetQueryable();
            
            if (request.SortOrder?.ToLower() == "desc")
                query = query.OrderByDescending(Review.GetSortProperty(request.SortColumn));
            else
                query = query.OrderBy(Review.GetSortProperty(request.SortColumn));
            
            var configuration = new MapperConfiguration(cfg => cfg.CreateProjection<Review, ReviewListDto>()
                .ForMember(d => d.IsPlussed, o => o.MapFrom(s => s.UserReviewPluses.Any(x => x.UserId == userId))));
            
            var result = query.ProjectTo<ReviewListDto>(configuration);
            return await PagedList<ReviewListDto>.CreateAsync(result, request.Page, request.PageSize);
        }
        
        public new async Task<PagedList<ReviewListDto>> GetAsync(PagedListQuery<Review> request)
        {
            return await GetAsync(GetQueryable(), request);
        }
        
        public new async Task<ReviewDetailsDto> GetByIdAsync(int id, int userId)
        {
            var query = GetQueryable();
            
            var configuration = new MapperConfiguration(cfg => cfg.CreateProjection<Review, ReviewDetailsDto>()
                .ForMember(d => d.IsPlussed, o => o.MapFrom(s => s.UserReviewPluses.Any(x => x.UserId == userId))));
            
            var result = query.ProjectTo<ReviewDetailsDto>(configuration);
            return await result.FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public new async Task<ReviewDetailsDto> GetByIdAsync(int id)
        {
            return await GetByIdAsync(GetQueryable(), id);
        }

        public async Task AddPlus(int userId, int reviewId)
        {
            var plus = await _context.UserReviewPluses.FirstOrDefaultAsync(x => x.UserId == userId && x.ReviewId == reviewId);

            if (plus != null) return;
            
            await _context.UserReviewPluses.AddAsync(new UserReviewPlus(userId, reviewId));
            await _context.SaveChangesAsync();
        }
        
        public async Task RemovePlus(int userId, int reviewId)
        {
            var plus = await _context.UserReviewPluses.FirstOrDefaultAsync(x => x.UserId == userId && x.ReviewId == reviewId);

            if (plus == null) return;
            
            _context.UserReviewPluses.Remove(plus);
            await _context.SaveChangesAsync();
        }
        
        private IQueryable<Review> GetQueryable()
        {
            var query = _repository.Entities;
            
            return query
                .Include(x => x.UserReviewPluses)
                .Include(x => x.User);
        }

        public async Task AddAsync(ReviewUpdateDto model, int userId)
        {
            var entity = _mapper.Map<Review>(model);
            entity.UserId = userId;
            await _repository.AddAsync(entity);
        }
        
        public async Task UpdateAsync(ReviewUpdateDto model, int userId)
        {
            var entity = _mapper.Map<Review>(model);

            if (entity.UserId != userId) return;
            
            await _repository.UpdateAsync(entity);
        }
    }
}