using AutoMapper;
using Backend.Data.Entities;
using Backend.Models;
using Backend.Models.Author;
using Backend.Repositories;
using Backend.Services.Base;

namespace Backend.Services
{
    public class AuthorService : ServiceAsync<Author, AuthorListDto, AuthorDetailsDto, AuthorUpdateDto>
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
            entity.PicturePath = $"pictures/author_{model.Id}.jpg";
            await _repository.UpdateAsync(entity);
        }
    }
}