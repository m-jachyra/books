using AutoMapper;
using Backend.Data.Entities;
using Backend.Models;
using Backend.Models.Author;
using Backend.Repositories;
using Backend.Services.Base;
using Backend.Storage;

namespace Backend.Services
{
    public class AuthorService : ServiceWithPhotosAsync<Author, AuthorListDto, AuthorDetailsDto, AuthorUpdateDto>
    {
        private readonly IStorageService _storageService;
        private readonly IRepositoryAsync<Author> _repository;
        private readonly IMapper _mapper;
        public AuthorService(IRepositoryAsync<Author> repository, IMapper mapper, IStorageService storageService) : base(repository, mapper, storageService)
        {
            _repository = repository;
            _mapper = mapper;
            _storageService = storageService;
        }
    }
}