using AutoMapper;
using Backend.Data.Entities;
using Backend.Models;
using Backend.Repositories;
using Backend.Services.Base;

namespace Backend.Services
{
    public class AuthorService : ServiceAsync<Author, AuthorDto>, IPictureService
    {
        private readonly IRepositoryAsync<Author> _repository;
        private readonly IMapper _mapper;
        public AuthorService(IRepositoryAsync<Author> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task UpdatePicturePath(PictureDto model)
        {
            var entity = await _repository.GetByIdAsync(model.Id);
            entity.PicturePath = $"picture/author_{model.Id}";
            await _repository.UpdateAsync(entity);
        }
    }
}