using AutoMapper;
using Backend.Data.Entities;
using Backend.Models;
using Backend.Models.Base;
using Backend.Repositories;
using Backend.Storage;

namespace Backend.Services.Base
{
    public class ServiceWithPhotosAsync<TEntity, TListDto, TDetailsDto, TUpdateDto> : ServiceAsync<TEntity, TListDto, TDetailsDto, TUpdateDto>
        where TEntity : class, IHasId, ISortable<TEntity>, IHasPicture
        where TListDto : IListDto
        where TDetailsDto : IDetailsDto
        where TUpdateDto : IUpdateDto
    {
        private readonly IStorageService _storageService;
        private readonly IRepositoryAsync<TEntity> _repository;
        
        public ServiceWithPhotosAsync(IRepositoryAsync<TEntity> repository, IMapper mapper, IStorageService storageService) : base(repository, mapper)
        {
            _repository = repository;
            _storageService = storageService;
        }

        public async Task UpdatePicturePath(PictureDto model)
        {
            var entity = await _repository.GetByIdAsync(model.Id);
            
            await _storageService.DeleteFileAsync(entity.PicturePath);
            
            var fileName = $"picture_{model.Id}_{DateTime.UtcNow.ToString("ddMMyyyyhhmmss")}{Path.GetExtension(model.File.FileName)}";
            await _storageService.UploadFileAsync(model.File, fileName);

            entity.PicturePath = fileName;
            await _repository.UpdateAsync(entity);
        }
    }
}