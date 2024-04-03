using AutoMapper;
using Backend.Models.Base;

namespace Backend.Models.Genre
{
    public class GenreUpdateDto : IUpdateDto, IMapFrom
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GenreUpdateDto, Data.Entities.Genre>();
        }
    }
}