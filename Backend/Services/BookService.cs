using AutoMapper;
using AutoMapper.QueryableExtensions;
using Backend.Data.Entities;
using Backend.Helpers;
using Backend.Models;
using Backend.Models.Book;
using Backend.Repositories;
using Backend.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BookService : ServiceAsync<Book, BookListDto, BookDetailsDto, BookUpdateDto>
    {
        private readonly IRepositoryAsync<Book> _repository;
        private readonly IMapper _mapper;
        public BookService(IRepositoryAsync<Book> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task UpdatePicturePath(PictureDto model)
        {
            var entity = await _repository.GetByIdAsync(model.Id);
            entity.PicturePath = $"pictures/book_{model.Id}.jpg";
            await _repository.UpdateAsync(entity);
        }
        
        public new async Task<PagedList<BookListDto>> GetAsync(PagedListQuery<Book> request)
        {
            return await GetAsync(GetQueryable(), request);
        }
        
        public new async Task<BookDetailsDto> GetByIdAsync(int id)
        {
            return await GetByIdAsync(GetQueryable(), id);
        }

        private IQueryable<Book> GetQueryable()
        {
            var query = _repository.Entities;
            
            return query
                .Include(x => x.Author)
                .Include(x => x.Genre);
        }
    }
}