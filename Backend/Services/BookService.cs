using AutoMapper;
using AutoMapper.QueryableExtensions;
using Backend.Data.Entities;
using Backend.Helpers;
using Backend.Models;
using Backend.Models.Book;
using Backend.Repositories;
using Backend.Services.Base;
using Backend.Storage;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class BookService : ServiceWithPhotosAsync<Book, BookListDto, BookDetailsDto, BookUpdateDto>
    {
        private readonly IRepositoryAsync<Book> _repository;
        public BookService(IRepositoryAsync<Book> repository, IMapper mapper, IStorageService storageService) : base(repository, mapper, storageService)
        {
            _repository = repository;
        }
        
        public new async Task<PagedList<BookListDto>> GetAsync(PagedListQuery<Book> request)
        {
            return await GetMappedPagedListAsync(request, Queryable);
        }
        
        public new async Task<BookDetailsDto> GetByIdAsync(int id)
        {
            return await GetByIdAsync(id, Queryable);
        }

        private IQueryable<Book> Queryable =>
            _repository.Entities
                .Include(x => x.Author)
                .Include(x => x.Genre);
    }
}