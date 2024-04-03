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
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

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
            var query = Queryable
                .OrderByDescending(x => x.UserReviewPluses.Count())
                .Take(10);
            
            var configuration = GetConfiguration(userId);
            
            return await GetMappedListAsync(query, configuration);
        }

        public async Task<List<ReviewListDto>> GetTopReviews(int? userId)
        {
            var query = Queryable
                .OrderByDescending(x => x.UserReviewPluses.Count())
                .Take(10);
            
            var configuration = GetConfiguration(userId);
            
            return await GetMappedListAsync(query, configuration);
        }
        
        public async Task<PagedList<ReviewListDto>> GetByBookId(int id, PagedListQuery<Review> request, int? userId)
        {
            var configuration = GetConfiguration(userId);

            return await GetMappedPagedListAsync(request, Queryable.Where(x => x.BookId == id), configuration);
        }
        
        public new async Task<PagedList<ReviewListDto>> GetAsync(PagedListQuery<Review> request, int userId)
        {
            var configuration = GetConfiguration(userId);

            return await GetMappedPagedListAsync(request, Queryable, configuration);
        }
        
        public async Task<ReviewDetailsDto> GetByIdAsync(int id, int userId)
        {
            var configuration = GetConfiguration(userId);
            
            return await GetByIdAsync(id, Queryable, configuration);
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
        
        // [Private]
        private IConfigurationProvider GetConfiguration(int? userId)
        {
            return userId != null ?
                new MapperConfiguration(cfg => cfg.CreateProjection<Review, ReviewListDto>()
                    .ForMember(d => d.IsPlussed, o => o.MapFrom(s => s.UserReviewPluses.Any(x => x.UserId == userId)))) :
                _mapper.ConfigurationProvider;
        }
        
        private IQueryable<Review> Queryable =>
            _repository.Entities
                .Include(x => x.UserReviewPluses)
                .Include(x => x.User);
    }
}