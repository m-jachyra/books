using AutoMapper;
using Backend.Data.Entities;
using Backend.Models.Genre;
using Backend.Repositories;
using Backend.Services.Base;

namespace Backend.Services
{
    public class GenreService : ServiceAsync<Genre, GenreListDto, GenreDetailsDto, GenreUpdateDto>
    {
        public GenreService(IRepositoryAsync<Genre> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}