using AutoMapper;
using Backend.Data.Entities;
using Backend.Models;
using Backend.Repositories;
using Backend.Services.Base;

namespace Backend.Services
{
    public class BookService : ServiceAsync<Book, BookDto>, IPictureService
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
    }
}