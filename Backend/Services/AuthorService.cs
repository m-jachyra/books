using AutoMapper;
using Backend.Data.Entities;
using Backend.Models;
using Backend.Models.Author;
using Backend.Repositories;
using Backend.Services.Base;
using Backend.Storage;

namespace Backend.Services
{
    public class AuthorService : ServiceAsync<Author, AuthorListDto, AuthorDetailsDto, AuthorUpdateDto>
    {
        private readonly IStorageService _storageService;
        private readonly IRepositoryAsync<Author> _repository;
        private readonly IMapper _mapper;
        public AuthorService(IRepositoryAsync<Author> repository, IMapper mapper, IStorageService storageService) : base(repository, mapper)
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
    }
}