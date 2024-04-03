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
    public class BookService : ServiceAsync<Book, BookListDto, BookDetailsDto, BookUpdateDto>
    {
        private readonly IStorageService _storageService;
        private readonly IRepositoryAsync<Book> _repository;
        private readonly IMapper _mapper;
        public BookService(IRepositoryAsync<Book> repository, IMapper mapper, IStorageService storageService) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task UpdatePicturePath(PictureDto model)
        {
            var entity = await _repository.GetByIdAsync(model.Id);
            
            await _storageService.DeleteFileAsync(entity.PicturePath);
            
            var fileName = $"picture_{model.Id}_{DateTime.UtcNow})";
            await _storageService.UploadFileAsync(model.File, fileName);
            
            entity.PicturePath = $"pictures/author_{model.Id}.jpg";
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