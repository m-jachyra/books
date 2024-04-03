using AutoMapper;
using Backend.Models.Base;

namespace Backend.Models.Genre
{
    public class GenreDetailsDto : IDetailsDto, IMapFrom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Data.Entities.Genre, GenreDetailsDto>();
        }
    }
}