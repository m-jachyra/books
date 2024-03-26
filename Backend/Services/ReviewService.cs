using AutoMapper;
using Backend.Data.Entities;
using Backend.Models;
using Backend.Repositories;
using Backend.Services.Base;
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


        public async Task<ReviewDto> GetByBookId(int id, int pageIndex, int pageSize)
        {
            return _mapper.Map<ReviewDto>(await _repository.Entities.Where(x => x.BookId == id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
        }
    }
}